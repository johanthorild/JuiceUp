using Domain;
using Domain.Entities;
using Domain.Repositories;

using MediatR;

namespace Application.Commands;

public class CreateChargerCommandHandler : IRequestHandler<CreateChargerCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IChargerRepository _chargerRepository;

    public CreateChargerCommandHandler(
        IUnitOfWork unitOfWork,
        IChargerRepository chargerRepository)
    {
        _unitOfWork = unitOfWork;
        _chargerRepository = chargerRepository;
    }

    public async Task<int> Handle(
        CreateChargerCommand command,
        CancellationToken cancellationToken)
    {
        var charger = new Charger(
            command.StationId,
            command.ChargerSpeedId);

        _chargerRepository.Insert(charger);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return charger.Id;
    }
}

