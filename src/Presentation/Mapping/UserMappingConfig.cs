using Application.Commands;

using Domain.Entities;

using Mapster;

using Presentation.Requests;
using Presentation.Responses;

namespace Presentation.Mapping;
public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateUserRequest, UpdateUserCommand>();

        config.NewConfig<User, UserResponse>()
            .Map(dest => dest.Roles, src => src.UserRoles)
            .Map(dest => dest.Cars, src => src.UserCars)
            .Map(dest => dest.Reservations, src => src.Reservations);

        config.NewConfig<UserRole, UserRoleResponse>()
            .Map(dest => dest.Name, src => src.Role.Name)
            .Map(dest => dest.Id, src => src.Role.Id);

        config.NewConfig<UserCar, UserCarResponse>()
            .Map(dest => dest.CarModelName, src => src.Car.Name)
            .Map(dest => dest.Capacity, src => src.Car.Capacity);

        config.NewConfig<Reservation, ReservationResponse>()
            .Map(dest => dest.ChargerId, src => src.ChargerId);
    }
}
