using Application.Commands;

using Domain.Entities;

using Mapster;

using Presentation.Requests;
using Presentation.Responses;

namespace Presentation.Mapping;
public class StationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateStationRequest, CreateStationCommand>();
        config.NewConfig<UpdateStationRequest, UpdateStationCommand>();

        config.NewConfig<Station, StationResponse>()
            .Map(dest => dest.Chargers, src => src.Chargers);

        //config.NewConfig<UserRole, UserRoleResponse>()
        //    .Map(dest => dest.Name, src => src.Role.Name)
        //    .Map(dest => dest.Id, src => src.Role.Id);

    }
}
