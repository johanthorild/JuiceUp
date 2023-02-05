using MediatR;

namespace Application.Commands;

public sealed record LoginCommand(
    string Email,
    string PasswordBase64) : IRequest<LoginResult>;
