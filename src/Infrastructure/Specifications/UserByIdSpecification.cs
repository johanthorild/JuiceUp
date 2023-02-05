using Domain.Entities;

using Infrastructure.Specifications.NewFolder;

namespace Infrastructure.Specifications;
internal class UserByIdSpecification : BaseSpecification<User>
{
    public UserByIdSpecification(Guid id)
        : base(user => user.Id == id)
    {
    }
}
