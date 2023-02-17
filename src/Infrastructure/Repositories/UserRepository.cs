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

    public async Task<bool> IsUserWithEmailExisting(string email)
    {
        return await _dbContext.Users.AnyAsync(x => x.Email == email);
    }

    public User? Insert(User user)
    {
        var entry = _dbContext?.Set<User>().Add(user).Entity;
        return entry is not null ? entry : null;
    }

    public User? Update(User user)
    {
        var entry = _dbContext?.Set<User>().Update(user).Entity;
        return entry is not null ? entry : null;
    }

    public void Delete(Guid id)
    {
        var user = _dbContext?.Set<User>()
            .Include(u => u.UserRoles)
            .Include(u => u.UserCars)
            .Include(u => u.Reservations)
            .FirstOrDefault(x => x.Id == id);

        if (user is not null)
        {
            _dbContext?.Set<UserRole>().RemoveRange(user.UserRoles);
            _dbContext?.Set<UserCar>().RemoveRange(user.UserCars);
            _dbContext?.Set<Reservation>().RemoveRange(user.Reservations);
            _dbContext?.Set<User>().Remove(user);
        }
    }

    // TODO: Apply Specification pattern
    //private IQueryable<User> ApplySpecification(ISpecification<User> specification)
    //{
    //    return SpecificationEvaluator<User>.GetQuery(_dbContext.Set<User>(), specification);
    //}
}
