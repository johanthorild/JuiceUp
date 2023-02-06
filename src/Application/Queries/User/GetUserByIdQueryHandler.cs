using Application.Commands;

using Domain.Entities;
using Domain.Repositories;

using MediatR;

namespace Application.Queries;
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(
        GetUserByIdQuery command,
        CancellationToken cancellationToken)
    {
        var existing = await _userRepository.GetById(command.Id);
        return existing is null ?
            throw new NotImplementedException(nameof(RegisterCommand))
            :
            existing;
    }
}

