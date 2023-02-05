using Domain.Entities;
using Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetById(Guid id)
    {
        var user = await _dbContext.Users
            .Include(u => u.UserRoles)
                .ThenInclude(u => u.Role)
            .Include(u => u.UserCars)
                .ThenInclude(u => u.Car)
            .FirstOrDefaultAsync(x => x.Id == id);

        return user is not null ? user : null;
    }

    public async Task<User?> GetByEmail(string email)
    {
        var user = await _dbContext.Users
            .Include(u => u.UserRoles)
                .ThenInclude(u => u.Role)
            .Include(u => u.UserCars)
                .ThenInclude(u => u.Car)
            .FirstOrDefaultAsync(x => x.Email == email);

        return user is not null ? user : null;
    }

    public void Insert(User user)
    {
        _dbContext?.Set<User>().Add(user);
    }

    public void Update(User user)
    {
        _dbContext?.Set<User>().Update(user);
    }

    public async void Delete(Guid id)
    {
        _dbContext?.Set<User>().Remove(
            await _dbContext.Users
            .SingleAsync(x => x.Id == id)
            );
    }

    // TODO: Apply Specification pattern
    //private IQueryable<User> ApplySpecification(ISpecification<User> specification)
    //{
    //    return SpecificationEvaluator<User>.GetQuery(_dbContext.Set<User>(), specification);
    //}
}
