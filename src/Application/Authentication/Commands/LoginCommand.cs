using MediatR;

namespace Application.Authentication.Commands;

public sealed record LoginCommand(
    string Email,
    string PasswordBase64) : IRequest<AuthenticationResult>;
