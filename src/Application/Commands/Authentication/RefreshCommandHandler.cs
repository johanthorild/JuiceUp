using Application.Dtos;
using Application.Providers;

using Domain;
using Domain.Repositories;

using MediatR;

namespace Application.Commands;

public class RefreshCommandHandler : IRequestHandler<RefreshCommand, RefreshTokenResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserRepository _userRepository;

    public RefreshCommandHandler(
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
        RefreshTokenResult command,
        CancellationToken cancellationToken)
    {
        var tokenValidationParameters = _authOptions.TokenValidationParameters ?? throw new InvalidOperationException($"Cannot get {nameof(AuthOptions.TokenValidationParameters)} from DI-provided configuration. This indicates a bug in the backend.");

    }
}

