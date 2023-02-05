using Application.Commands;

using Domain;
using Domain.Entities;
using Domain.Repositories;

using MediatR;

namespace Application.Queries;
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
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

