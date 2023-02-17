using Application.Dtos;

using Domain;
using Domain.Repositories;

using MediatR;

namespace Application.Commands;

public class CalculateRouteCommandHandler : IRequestHandler<CalculateRouteCommand, CalculateRouteResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStationRepository _stationRepository;

    public CalculateRouteCommandHandler(
        IUnitOfWork unitOfWork,
        IStationRepository stationRepository)
    {
        _unitOfWork = unitOfWork;
        _stationRepository = stationRepository;
    }

    public Task<CalculateRouteResult> Handle(
        CalculateRouteCommand command,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(new CalculateRouteResult());
    }
}

