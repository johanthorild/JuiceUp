using Application.Commands;
using Application.Queries;

using Carter;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using Presentation.Requests;

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
        app.MapGet("/{id}", async (
            [FromRoute] Guid id,
            HttpContext ct,
            ISender sender) =>
        {
            var result = await sender.Send(new GetUserByIdQuery(id));
            return Results.Ok(result);
        });

        app.MapPut("/{id}", async (
            [FromRoute] Guid id,
            [FromBody] UpdateUserRequest request,
            ISender sender) =>
        {
            var result = await sender.Send(new UpdateUserCommand(
                id,
                request.Email,
                request.Firstname,
                request.Lastname,
                request.PasswordBase64));
            return Results.Ok(result);
        });
    }
}
