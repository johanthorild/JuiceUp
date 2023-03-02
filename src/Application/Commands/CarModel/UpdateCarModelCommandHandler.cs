using Application.Providers;

using Domain;
using Domain.Repositories;

using MediatR;

namespace Application.Commands;

public class UpdateCarModelCommandHandler : IRequestHandler<UpdateCarModelCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextUserProvider _httpContextUserProvider;
    private readonly ICarModelRepository _carModelRepository;

    public UpdateCarModelCommandHandler(
        IUnitOfWork unitOfWork,
        IHttpContextUserProvider httpContextUserProvider,
        ICarModelRepository carModelRepository)
    {
        _unitOfWork = unitOfWork;
        _httpContextUserProvider = httpContextUserProvider;
        _carModelRepository = carModelRepository;
    }

    public async Task<int> Handle(
        UpdateCarModelCommand command,
        CancellationToken cancellationToken)
    {
        // Only admins can update the car model
        if (!_httpContextUserProvider.IsAdmin)
            throw new NotImplementedException(nameof(UpdateCarModelCommand));

        // Get existing car model
        var existing = await _carModelRepository.GetById(command.Id);
        if (existing is null)
            throw new NotImplementedException(nameof(UpdateCarModelCommand));

        // Set properties
        existing.SetName(command.Name);
        existing.SetCapacity(command.Capacity);
        existing.SetRealRange(command.RealRange);

        _carModelRepository.Update(existing);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return existing.Id;
    }
}

