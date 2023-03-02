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
public class CarModelModule : CarterModule
{
    public CarModelModule()
        : base("/api/carmodel")
    {
        WithGroupName("CarModel");
        RequireAuthorization();
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (
            ISender sender,
            IMapper mapper) =>
        {
            var result = await sender.Send(new GetCarModelsQuery());
            return Results.Ok(mapper.Map<IReadOnlyList<CarModelResponse>>(result));
        })
        .AllowAnonymous();

        app.MapPost("/", async (
            [FromBody] CreateCarModelRequest request,
            ISender sender,
            IMapper mapper) =>
        {
            var command = mapper.Map<CreateCarModelCommand>(request);
            return Results.Ok(await sender.Send(command));
        });

        app.MapPut("/", async (
            [FromBody] UpdateCarModelRequest request,
            ISender sender,
            IMapper mapper) =>
        {
            var command = mapper.Map<UpdateCarModelCommand>(request);
            return Results.Ok(await sender.Send(command));
        });

        app.MapDelete("/{id}", async (
            [FromRoute] int id,
            ISender sender,
            IMapper mapper) =>
        {
            var result = await sender.Send(new DeleteCarModelCommand(id));
            return Results.Ok(result);
        });
    }
}
