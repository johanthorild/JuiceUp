using Application.Dtos;
using Application.Helpers;
using Application.Providers;

using Domain;
using Domain.Repositories;

using MediatR;

namespace Application.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserRepository _userRepository;

    public LoginCommandHandler(
        IJwtTokenProvider jwtTokenProvider,
        IDateTimeProvider dateTimeProvider,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _jwtTokenProvider = jwtTokenProvider;
        _dateTimeProvider = dateTimeProvider;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResult> Handle(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        var existing = await _userRepository.GetByEmail(command.Email);
        if (existing is null)
            throw new NotImplementedException(nameof(LoginCommand));

        if (existing.LockedUntil != null && existing.LockedUntil.Value > _dateTimeProvider.UtcNow)
            throw new NotImplementedException(nameof(LoginCommand));

        if (!PasswordHelper.VerifyPassword(command.PasswordBase64, existing.Password, existing.Salt))
        {
            // TODO: Add failed attempt and lock user if too many failed attempts
            throw new NotImplementedException(nameof(LoginCommand));
        }

        existing.ResetFailedLogins();
        await _unitOfWork.SaveChangesWithoutChangeTrackingAsync(cancellationToken);

        return await _jwtTokenProvider.GenerateToken(existing);
    }
}

