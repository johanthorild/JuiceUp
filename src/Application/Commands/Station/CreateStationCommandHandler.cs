using Domain;
using Domain.Entities;
using Domain.Repositories;

using MediatR;

namespace Application.Commands;

public class CreateStationCommandHandler : IRequestHandler<CreateStationCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStationRepository _stationRepository;

    public CreateStationCommandHandler(
        IUnitOfWork unitOfWork,
        IStationRepository stationRepository)
    {
        _unitOfWork = unitOfWork;
        _stationRepository = stationRepository;
    }

    public async Task<int> Handle(
        CreateStationCommand command,
        CancellationToken cancellationToken)
    {
        var station = new Station(
            command.Name,
            command.Address,
            command.ZipCode,
            command.City,
            command.Latitude,
            command.Longitude,
            command.OpenTime,
            command.CloseTime,
            command.HasRestaurant,
            command.HasConference,
            command.HasPersonel,
            command.HasRestroom);

        _stationRepository.Insert(station);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return station.Id;
    }
}

