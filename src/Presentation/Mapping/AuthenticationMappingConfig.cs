using Application.Commands;
using Application.Dtos;

using Mapster;

using Presentation.Requests;
using Presentation.Responses;

namespace Presentation.Mapping;
public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginCommand>();

        config.NewConfig<LoginResult, LoginResponse>();
        config.NewConfig<RefreshTokenResult, RefreshTokenResponse>();
    }
}
