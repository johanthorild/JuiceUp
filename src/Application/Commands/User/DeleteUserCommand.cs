using MediatR;

namespace Application.Commands;

public sealed record DeleteUserCommand(Guid Id) : IRequest<Guid>;
