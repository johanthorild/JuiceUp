namespace Domain;

public interface IUnitOfWork
{
    int SaveChanges();
    int SaveChangesWithUser(string email);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesWithUserAsync(string email, CancellationToken cancellationToken = default);
    Task<int> SaveChangesWithoutChangeTrackingAsync(CancellationToken cancellationToken = default);
}
