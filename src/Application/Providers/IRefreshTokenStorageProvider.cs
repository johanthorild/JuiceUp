using Application.Dtos;

namespace Application.Providers;

public interface IRefreshTokenStorageProvider
{
    Task<RefreshTokenResult?> GetByTokenStringAsync(string tokenString);
    Task DeleteAllForUserAsync(string userName);
    Task DeleteTokenAsync(string tokenString);
    Task AddTokenForUserAsync(RefreshTokenResult refreshToken);
}
