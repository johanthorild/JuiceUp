using Domain.Entities;

using MediatR;

namespace Application.Commands;

public sealed record UpdateUserCommand(
    Guid Id,
    string Email,
    string Firstname,
    string Lastname,
    string PasswordBase64) : IRequest<User>;
