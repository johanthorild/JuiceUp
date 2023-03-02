using Application.Providers;

using Domain;
using Domain.Repositories;

using MediatR;

namespace Application.Commands;

public class DeleteCarModelCommandHandler : IRequestHandler<DeleteCarModelCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextUserProvider _httpContextUserProvider;
    private readonly ICarModelRepository _carModelRepository;

    public DeleteCarModelCommandHandler(
        IUnitOfWork unitOfWork,
        IHttpContextUserProvider httpContextUserProvider,
        ICarModelRepository carModelRepository)
    {
        _unitOfWork = unitOfWork;
        _httpContextUserProvider = httpContextUserProvider;
        _carModelRepository = carModelRepository;
    }

    public async Task<int> Handle(
        DeleteCarModelCommand command,
        CancellationToken cancellationToken)
    {
        // Only admins can delete the car model
        if (!_httpContextUserProvider.IsAdmin)
            throw new NotImplementedException(nameof(DeleteCarModelCommand));

        _carModelRepository.Delete(command.Id);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return command.Id;
    }
}

