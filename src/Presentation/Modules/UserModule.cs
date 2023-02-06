using Application.Commands;
using Application.Queries;

using Carter;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using Presentation.Requests;
using Presentation.Responses;

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
            ISender sender,
            IMapper mapper) =>
        {
            var result = await sender.Send(new GetUserByIdQuery(id));
            return Results.Ok(mapper.Map<UserResponse>(result));
        });

        app.MapPut("/", async (
            [FromBody] UpdateUserRequest request,
            ISender sender,
            IMapper mapper) =>
        {
            var command = mapper.Map<UpdateUserCommand>(request);
            return Results.Ok(await sender.Send(command));
        });
    }
}
