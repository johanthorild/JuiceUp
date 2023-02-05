using Carter;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Modules;
public class UserModule : CarterModule
{

    public UserModule()
        : base("/api/user")
    {
        WithGroupName("User");
        RequireAuthorization();
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", (
            [FromQuery] Guid id,
            ISender sender) =>
        {
            //var result = await _sender.Send(new GetUserByIdQuery());
            return Results.Ok();
        });
    }
}
