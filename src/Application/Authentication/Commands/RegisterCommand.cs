using MediatR;

namespace Application.Authentication.Commands;

public sealed record RegisterCommand(
    string Email,
    string Firstname,
    string Lastname,
    string PasswordBase64) : IRequest<AuthenticationResult>;
