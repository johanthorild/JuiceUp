using Application.Helpers;
using Application.Providers;

using Domain;
using Domain.Entities;
using Domain.Repositories;

using MediatR;

namespace Application.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, LoginResult>
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

    public async Task<LoginResult> Handle(
        RegisterCommand command,
        CancellationToken cancellationToken)
    {
        var existing = await _userRepository.GetByEmail(command.Email);
        if (existing is not null)
            throw new NotImplementedException(nameof(RegisterCommand));

        var (salt, hashedPassword) = PasswordHelper.GenerateHashedPasswordWithSalt(command.PasswordBase64);

        var user = new User(command.Email, command.Firstname, command.Lastname, hashedPassword, salt);

        _userRepository.Insert(user);

        await _unitOfWork.SaveChangesWithoutChangeTrackingAsync(cancellationToken);

        var token = _jwtTokenProvider.GenerateToken(user);

        return new LoginResult(
            user.Id,
            user.Email,
            token
        );
    }
}

