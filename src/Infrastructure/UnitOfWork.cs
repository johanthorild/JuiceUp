using Application.Providers;

using Domain;

using Infrastructure.Models.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IHttpContextUserProvider _httpContextUserProvider;

    public UnitOfWork(
        AppDbContext dbContext,
        IDateTimeProvider dateTimeProvider,
        IHttpContextUserProvider httpContextUserProvider)
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
        _httpContextUserProvider = httpContextUserProvider;
    }

    public int SaveChanges()
    {
        UpdateChangeTracking();
        return _dbContext.SaveChanges();
    }

    public int SaveChangesWithUser(string email)
    {
        UpdateChangeTracking(email);
        return _dbContext.SaveChanges();
    }

    /// <summary>
    /// Save changes to DB using change tracking for currently logged in user.
    /// </summary>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await SaveChangesInternalAsync(false, cancellationToken);
    }

    /// <summary>
    /// Save changes to DB without any change tracking.
    /// </summary>
    public async Task<int> SaveChangesWithoutChangeTrackingAsync(CancellationToken cancellationToken = default)
    {
        return await SaveChangesInternalAsync(true, cancellationToken);
    }

    /// <summary>
    /// Save changes to DB using change tracking with email.
    /// </summary>
    public async Task<int> SaveChangesWithUserAsync(string email, CancellationToken cancellationToken = default)
    {
        UpdateChangeTracking(email);
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<int> SaveChangesInternalAsync(bool ignoreChangeTracking, CancellationToken cancellationToken)
    {
        if (!ignoreChangeTracking)
            UpdateChangeTracking();

        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Update all changed entities in regards of when the change was made and by which user.
    /// </summary>
    private void UpdateChangeTracking(string? email = null)
    {
        var changeset = _dbContext.ChangeTracker.Entries<IChangeTracked>();
        if (changeset != null)
        {
            foreach (var entityEntry in changeset.Where(x => x.State != EntityState.Unchanged))
            {
                entityEntry.Entity.LastChanged = _dateTimeProvider.Now;
                entityEntry.Entity.LastChangedBy = email ?? _httpContextUserProvider.Email;

                if (!_dbContext.ChangeTracker.AutoDetectChangesEnabled)
                {
                    entityEntry.Property(x => x.LastChanged).IsModified = true;
                    entityEntry.Property(x => x.LastChangedBy).IsModified = true;
                }
            }
        }
    }
}
