using Carter;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation;
public class UserModule : CarterModule
{
    public UserModule()
        : base("/user")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", (ISender sender) => Results.Ok());
    }
}
