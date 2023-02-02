using Application.Helpers;
using Application.Providers;

using Domain.Repositories;

using MediatR;

namespace Application.Authentication.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthenticationResult>
{
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly IUserRepository _userRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public LoginCommandHandler(
        IJwtTokenProvider jwtTokenProvider,
        IUserRepository userRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _jwtTokenProvider = jwtTokenProvider;
        _userRepository = userRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<AuthenticationResult> Handle(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask; // remove when handler uses async methods

        var existing = await _userRepository.GetByEmail(command.Email);
        if (existing is null)
            throw new NotImplementedException(nameof(LoginCommand));

        if (existing.LockedUntil != null && existing.LockedUntil.Value > _dateTimeProvider.UtcNow)
            throw new NotImplementedException(nameof(LoginCommand));

        if (!PasswordHelper.VerifyPassword(command.PasswordBase64, existing.Password, existing.Salt))
        {
            // TODO: Lock user if too many failed attempts

            throw new NotImplementedException(nameof(LoginCommand));
        }

        var token = _jwtTokenProvider.GenerateToken(existing);

        return new AuthenticationResult(
            existing.Id,
            existing.Email,
            token
        );
    }
}

