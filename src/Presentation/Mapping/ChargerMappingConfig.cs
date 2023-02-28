using Application.Commands;

using Domain.Entities;

using Mapster;

using Presentation.Requests;
using Presentation.Responses;

namespace Presentation.Mapping;

public class ChargerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateChargerRequest, CreateChargerCommand>();

        config.NewConfig<Charger, ChargerResponse>()
            .Map(dest => dest.StationId, src => src.Station.Id)
            .Map(dest => dest.ChargerSpeedId, src => src.ChargerSpeedId)
            .Map(dest => dest.ChargerSpeedKilowatt, src => src.ChargerSpeed.Kilowatt);
    }
}
