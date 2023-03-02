using MediatR;

namespace Application.Commands;

public sealed record UpdateCarModelCommand(
    int Id,
    string? Name,
    double? Capacity,
    double? RealRange) : IRequest<int>;
