using MediatR;

namespace Application.Commands;

public sealed record DeleteCarModelCommand(int Id) : IRequest<int>;
