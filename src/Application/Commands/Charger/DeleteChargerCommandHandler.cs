using Application.Providers;

using Domain;
using Domain.Repositories;

using MediatR;

namespace Application.Commands;

public class DeleteChargerCommandHandler : IRequestHandler<DeleteChargerCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextUserProvider _httpContextUserProvider;
    private readonly IChargerRepository _chargerRepository;

    public DeleteChargerCommandHandler(
        IUnitOfWork unitOfWork,
        IHttpContextUserProvider httpContextUserProvider,
        IChargerRepository chargerRepository)
    {
        _unitOfWork = unitOfWork;
        _httpContextUserProvider = httpContextUserProvider;
        _chargerRepository = chargerRepository;
    }

    public async Task<int> Handle(
        DeleteChargerCommand command,
        CancellationToken cancellationToken)
    {
        // Only admins can delete the stations
        if (!_httpContextUserProvider.IsAdmin)
            throw new NotImplementedException(nameof(DeleteStationCommand));

        _chargerRepository.Delete(command.Id);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return command.Id;
    }
}

