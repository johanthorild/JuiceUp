using Application.Providers;

using Domain;
using Domain.Repositories;

using MediatR;

namespace Application.Commands;

public class DeleteStationCommandHandler : IRequestHandler<DeleteStationCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextUserProvider _httpContextUserProvider;
    private readonly IStationRepository _stationRepository;

    public DeleteStationCommandHandler(
        IUnitOfWork unitOfWork,
        IHttpContextUserProvider httpContextUserProvider,
        IStationRepository stationRepository)
    {
        _unitOfWork = unitOfWork;
        _httpContextUserProvider = httpContextUserProvider;
        _stationRepository = stationRepository;
    }

    public async Task<int> Handle(
        DeleteStationCommand command,
        CancellationToken cancellationToken)
    {
        // Only admins can delete the stations
        if (!_httpContextUserProvider.IsAdmin)
            throw new NotImplementedException(nameof(DeleteStationCommand));

        _stationRepository.Delete(command.Id);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return command.Id;
    }
}

