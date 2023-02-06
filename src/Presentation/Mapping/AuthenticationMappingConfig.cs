using Application;
using Application.Commands;

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
        config.NewConfig<RefreshResult, RefreshResponse>();
    }
}
