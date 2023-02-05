using Application.Helpers;
using Application.Providers;

using Domain;
using Domain.Entities;
using Domain.Repositories;

using MediatR;

namespace Application.Authentication.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(
        IUnitOfWork unitOfWork,
        IJwtTokenProvider jwtTokenrovider,
        IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenProvider = jwtTokenrovider;
        _userRepository = userRepository;
    }

    public async Task<AuthenticationResult> Handle(
        RegisterCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask; // remove when handler uses async methods

        var existing = await _userRepository.GetByEmail(command.Email);
        if (existing is not null)
            throw new NotImplementedException(nameof(RegisterCommand));

        var (salt, hashedPassword) = PasswordHelper.GenerateHashedPasswordWithSalt(command.PasswordBase64);

        var user = new User(command.Email, command.Firstname, command.Lastname, hashedPassword, salt);

        _userRepository.Insert(user);

        await _unitOfWork.SaveChangesWithoutChangeTrackingAsync(cancellationToken);

        var token = _jwtTokenProvider.GenerateToken(user);

        return new AuthenticationResult(
            user.Id,
            user.Email,
            token
        );
    }
}

