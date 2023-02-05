using MediatR;

namespace Application.Commands;

public sealed record RegisterCommand(
    string Email,
    string Firstname,
    string Lastname,
    string PasswordBase64) : IRequest<LoginResult>;
