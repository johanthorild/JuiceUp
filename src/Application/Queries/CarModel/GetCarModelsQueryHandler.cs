using Domain.Entities;
using Domain.Repositories;

using MediatR;

namespace Application.Queries;
public class GetCarModelsQueryHandler : IRequestHandler<GetCarModelsQuery, IEnumerable<CarModel>>
{
    private readonly ICarModelRepository _carModelRepository;

    public GetCarModelsQueryHandler(
        ICarModelRepository carModelRepository)
    {
        _carModelRepository = carModelRepository;
    }

    public async Task<IEnumerable<CarModel>> Handle(
        GetCarModelsQuery command,
        CancellationToken cancellationToken)
    {
        var carModels = await _carModelRepository.GetAll();
        return carModels is null ?
            throw new NotImplementedException(nameof(GetCarModelsQuery))
            :
            carModels;
    }
}

