using Domain.Entities;

using Mapster;

using Presentation.Responses;

namespace Presentation.Mapping;
public class ReservationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Reservation, ReservationResponse>()
            .Map(dest => dest.ChargerId, src => src.ChargerId);

    }
}
