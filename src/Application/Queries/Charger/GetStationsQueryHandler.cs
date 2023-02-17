using Domain.Entities;
using Domain.Repositories;

using MediatR;

namespace Application.Queries;
public class GetChargersQueryHandler : IRequestHandler<GetChargersQuery, IEnumerable<Charger>>
{
    private readonly IChargerRepository _chargerRepository;

    public GetChargersQueryHandler(
        IChargerRepository chargerRepository)
    {
        _chargerRepository = chargerRepository;
    }

    public async Task<IEnumerable<Charger>> Handle(
        GetChargersQuery command,
        CancellationToken cancellationToken)
    {
        var chargers = await _chargerRepository.GetAll();
        return chargers is null ?
            throw new NotImplementedException(nameof(GetChargersQuery))
            :
            chargers;
    }
}

