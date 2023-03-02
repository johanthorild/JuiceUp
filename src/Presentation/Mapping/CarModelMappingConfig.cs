using Application.Commands;

using Domain.Entities;

using Mapster;

using Presentation.Requests;
using Presentation.Responses;

namespace Presentation.Mapping;
public class CarModelMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCarModelRequest, CreateCarModelCommand>();
        config.NewConfig<UpdateCarModelRequest, UpdateCarModelCommand>();

        config.NewConfig<CarModel, CarModelResponse>();
    }
}
