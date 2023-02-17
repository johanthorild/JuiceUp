using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetById(Guid id);
    Task<User?> GetByEmail(string email);
    Task<bool> IsUserWithEmailExisting(string email);
    User? Insert(User user);
    User? Update(User user);
    void Delete(Guid id);
}

