using Application.Providers;

using Domain;
using Domain.Repositories;

using MediatR;

namespace Application.Commands;

public class UpdateStationCommandHandler : IRequestHandler<UpdateStationCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextUserProvider _httpContextUserProvider;
    private readonly IStationRepository _stationRepository;

    public UpdateStationCommandHandler(
        IUnitOfWork unitOfWork,
        IHttpContextUserProvider httpContextUserProvider,
        IStationRepository stationRepository)
    {
        _unitOfWork = unitOfWork;
        _httpContextUserProvider = httpContextUserProvider;
        _stationRepository = stationRepository;
    }

    public async Task<int> Handle(
        UpdateStationCommand command,
        CancellationToken cancellationToken)
    {
        // Only admins can update the user
        if (!_httpContextUserProvider.IsAdmin)
            throw new NotImplementedException(nameof(UpdateStationCommand));

        // Get existing station
        var existing = await _stationRepository.GetById(command.Id);
        if (existing is null)
            throw new NotImplementedException(nameof(UpdateStationCommand));

        // Set properties
        existing.SetName(command.Name);
        existing.SetAddress(command.Address);
        existing.SetZipCode(command.ZipCode);
        existing.SetCity(command.City);
        existing.SetOpenTime(command.OpenTime);
        existing.SetCloseTime(command.CloseTime);

        _stationRepository.Update(existing);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return existing.Id;
    }
}

