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
public class ChargerModule : CarterModule
{
    public ChargerModule()
        : base("/api/charger")
    {
        WithGroupName("Charger");
        RequireAuthorization();
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (
            ISender sender,
            IMapper mapper) =>
        {
            var result = await sender.Send(new GetChargersQuery());
            return Results.Ok(mapper.Map<ChargerResponse>(result));
        }).RequireAuthorization();

        app.MapPost("/", async (
            [FromBody] CreateChargerRequest request,
            ISender sender,
            IMapper mapper) =>
        {
            var command = mapper.Map<CreateChargerCommand>(request);
            return Results.Ok(await sender.Send(command));
        });

        app.MapDelete("/{id}", async (
            [FromRoute] int id,
            ISender sender,
            IMapper mapper) =>
        {
            var result = await sender.Send(new DeleteChargerCommand(id));
            return Results.Ok(result);
        });
    }
}
