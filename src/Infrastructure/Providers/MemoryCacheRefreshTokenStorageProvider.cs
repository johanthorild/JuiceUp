using Application.Dtos;
using Application.Providers;

using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Providers;

public class MemoryCacheRefreshTokenStorageProvider : IRefreshTokenStorageProvider
{
    private const string CacheKeyUserXList = "RefreshToken_{0}";
    private readonly SemaphoreSlim _lockSemaphore = new(1, 1);
    private readonly Dictionary<string, RefreshTokenResult> _refreshTokensDict = new();
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheRefreshTokenStorageProvider(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task AddTokenForUserAsync(RefreshTokenResult refreshToken)
    {
        var cacheKey = string.Format(CacheKeyUserXList, refreshToken.UserName);

        await _lockSemaphore.WaitAsync();
        try
        {
            // First, add to dictionary of tokens for specific user

            if (!_memoryCache.TryGetValue<Dictionary<string, RefreshTokenResult>>(cacheKey, out var tokensForUserDict))
                tokensForUserDict = new Dictionary<string, RefreshTokenResult>();

            tokensForUserDict.Add(refreshToken.Token, refreshToken);

            using var entry = _memoryCache.CreateEntry(cacheKey);
            entry.AbsoluteExpiration = tokensForUserDict.Max(x => x.Value.Expires).AddDays(1); // Keep in cache one more day than the max expiration time
            entry.RegisterPostEvictionCallback(DeleteAllTokensForSingleUser); // Notice that this is called on another task!
            entry.Value = tokensForUserDict;

            // Then, add to large collection containing all tokens as well
            _refreshTokensDict.Add(refreshToken.Token, refreshToken);
        }
        finally
        {
            _lockSemaphore.Release();
        }
    }

    public async Task DeleteAllForUserAsync(string userName)
    {
        var cacheKey = string.Format(CacheKeyUserXList, userName);

        await _lockSemaphore.WaitAsync();
        try
        {
            if (!_memoryCache.TryGetValue<Dictionary<string, RefreshTokenResult>>(cacheKey, out var tokensForUserDict))
                return;

            DeleteAllTokensWithoutLock(tokensForUserDict.Values);
            _memoryCache.Remove(cacheKey);
        }
        finally
        {
            _lockSemaphore.Release();
        }
    }

    public async Task DeleteTokenAsync(string tokenString)
    {
        await _lockSemaphore.WaitAsync();
        try
        {
            if (!_refreshTokensDict.TryGetValue(tokenString, out var refreshToken))
                return; // Not found, nothing to remove

            _refreshTokensDict.Remove(tokenString);

            var cacheKey = string.Format(CacheKeyUserXList, refreshToken.UserName);

            if (!_memoryCache.TryGetValue<Dictionary<string, RefreshTokenResult>>(cacheKey, out var tokensForUserDict))
                return;

            if (!tokensForUserDict.Remove(tokenString))
                return;

            if (!tokensForUserDict.Any())
                _memoryCache.Remove(cacheKey); // Delete empty user dict from memory cache
        }
        finally
        {
            _lockSemaphore.Release();
        }
    }

    public async Task<RefreshTokenResult?> GetByTokenStringAsync(string tokenString)
    {
        await _lockSemaphore.WaitAsync();
        try
        {
            _refreshTokensDict.TryGetValue(tokenString, out var refreshToken);
            return refreshToken;
        }
        finally
        {
            _lockSemaphore.Release();
        }
    }

    private void DeleteAllTokensForSingleUser(object key, object value, EvictionReason reason, object state)
    {
        if (reason == EvictionReason.Removed || reason == EvictionReason.Replaced)
            return;

        var tokensForUserDict = (Dictionary<string, RefreshTokenResult>)value;

        _lockSemaphore.Wait();
        try
        {
            DeleteAllTokensWithoutLock(tokensForUserDict.Values);
        }
        finally
        {
            _lockSemaphore.Release();
        }
    }

    private void DeleteAllTokensWithoutLock(IEnumerable<RefreshTokenResult> tokens)
    {
        if (_lockSemaphore.CurrentCount != 0)
            throw new InvalidOperationException("Trying to delete all tokes for a specific user wihtout the expected lock - this is a bug in the backend!");

        foreach (var refreshToken in tokens)
        {
            _refreshTokensDict.Remove(refreshToken.Token);
        }
    }
}