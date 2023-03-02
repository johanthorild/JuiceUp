using Domain;
using Domain.Entities;
using Domain.Repositories;

using MediatR;

namespace Application.Commands;

public class CreateCarModelCommandHandler : IRequestHandler<CreateCarModelCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICarModelRepository _carModelRepository;

    public CreateCarModelCommandHandler(
        IUnitOfWork unitOfWork,
        ICarModelRepository carModelRepository)
    {
        _unitOfWork = unitOfWork;
        _carModelRepository = carModelRepository;
    }

    public async Task<int> Handle(
        CreateCarModelCommand command,
        CancellationToken cancellationToken)
    {
        var carModel = new CarModel(
            command.Name,
            command.Capacity,
            command.RealRange);

        _carModelRepository.Insert(carModel);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return carModel.Id;
    }
}

