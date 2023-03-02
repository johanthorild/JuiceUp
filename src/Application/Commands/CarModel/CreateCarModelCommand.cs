using MediatR;

namespace Application.Commands;

public sealed record CreateCarModelCommand(
    string Name,
    double Capacity,
    double RealRange) : IRequest<int>;
