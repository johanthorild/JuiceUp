using Domain.Entities;
using Domain.Repositories;

using Infrastructure.Specifications;
using Infrastructure.Specifications.Abstractions;

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
        var user = await ApplySpecification(new UserByIdSpecification(id))
            .FirstOrDefaultAsync();

        return user is not null ? new User()
        {
            Id = user.Id,
            Email = user.Email,
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            LastChanged = user.LastChanged,
            LastChangedBy = user.LastChangedBy
        }
        :
        null;
    }

    public async Task<User?> GetByEmail(string email)
    {
        var user = await _dbContext.Users
            .SingleOrDefaultAsync(x => x.Email == email);

        return user is not null ? new User()
        {
            Id = user.Id,
            Email = user.Email,
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            LastChanged = user.LastChanged,
            LastChangedBy = user.LastChangedBy
        }
        :
        null;
    }

    public void Insert(User user)
    {
        var userInsert = new Models.User()
        {
            Id = user.Id,
            Email = user.Email,
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            Password = user.Password,
            Salt = user.Salt
        };

        _dbContext?.Set<Models.User>().Add(userInsert);
    }

    public void Update(User user)
    {
        var userUpdate = new Models.User()
        {
            Id = user.Id,
            Email = user.Email,
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            Password = user.Password,
            Salt = user.Salt
        };

        _dbContext?.Set<Models.User>().Update(userUpdate);
    }

    public async void Delete(User user)
    {
        _dbContext?.Set<Models.User>().Remove(
            await _dbContext.Users
            .SingleAsync(x => x.Id == user.Id)
            );
    }

    private IQueryable<Models.User> ApplySpecification(ISpecification<Models.User> specification)
    {
        return SpecificationEvaluator<Models.User>.GetQuery(_dbContext.Set<Models.User>(), specification);
    }
}
