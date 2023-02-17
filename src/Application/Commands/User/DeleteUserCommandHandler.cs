using Application.Providers;

using Domain;
using Domain.Repositories;

using MediatR;

namespace Application.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextUserProvider _httpContextUserProvider;
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(
        IUnitOfWork unitOfWork,
        IHttpContextUserProvider httpContextUserProvider,
        IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _httpContextUserProvider = httpContextUserProvider;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(
        DeleteUserCommand command,
        CancellationToken cancellationToken)
    {
        // Only admins and the user itself can delete the user
        if (command.Id != _httpContextUserProvider.Id && !_httpContextUserProvider.IsAdmin)
            throw new NotImplementedException(nameof(DeleteUserCommand));

        _userRepository.Delete(command.Id);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return command.Id;
    }
}

