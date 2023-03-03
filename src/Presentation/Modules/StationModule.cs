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
public class StationModule : CarterModule
{
    public StationModule()
        : base("/api/station")
    {
        WithGroupName("Station");
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (
            ISender sender,
            IMapper mapper) =>
        {
            var result = await sender.Send(new GetStationsQuery());
            return Results.Ok(mapper.Map<IReadOnlyList<StationResponse>>(result));
        })
        .AllowAnonymous();

        app.MapPost("/", async (
            [FromBody] CreateStationRequest request,
            ISender sender,
            IMapper mapper) =>
        {
            var command = mapper.Map<CreateStationCommand>(request);
            return Results.Ok(await sender.Send(command));
        });

        app.MapPut("/", async (
            [FromBody] UpdateStationRequest request,
            ISender sender,
            IMapper mapper) =>
        {
            var command = mapper.Map<UpdateStationCommand>(request);
            return Results.Ok(await sender.Send(command));
        });

        app.MapDelete("/{id}", async (
            [FromRoute] int id,
            ISender sender,
            IMapper mapper) =>
        {
            var result = await sender.Send(new DeleteStationCommand(id));
            return Results.Ok(result);
        });
    }
}
