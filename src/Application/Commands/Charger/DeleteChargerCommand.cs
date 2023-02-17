using MediatR;

namespace Application.Commands;

public sealed record DeleteChargerCommand(int Id) : IRequest<int>;
