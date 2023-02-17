using Domain.Entities;
using Domain.Repositories;

using MediatR;

namespace Application.Queries;
public class GetStationsQueryHandler : IRequestHandler<GetStationsQuery, IEnumerable<Station>>
{
    private readonly IStationRepository _stationRepository;

    public GetStationsQueryHandler(
        IStationRepository stationRepository)
    {
        _stationRepository = stationRepository;
    }

    public async Task<IEnumerable<Station>> Handle(
        GetStationsQuery command,
        CancellationToken cancellationToken)
    {
        var stations = await _stationRepository.GetAll();
        return stations is null ?
            throw new NotImplementedException(nameof(GetStationsQuery))
            :
            stations;
    }
}

