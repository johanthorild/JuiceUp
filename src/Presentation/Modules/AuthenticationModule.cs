using Application.Authentication.Commands;

using Carter;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using Presentation.Requests;

namespace Presentation.Modules;
public class AuthenticationModule : CarterModule
{
    public AuthenticationModule()
        : base("/api/auth")
    {
        WithGroupName("Authentication");
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (
            [FromBody] LoginRequest request,
            ISender sender) =>
        {
            var result = await sender.Send(new LoginCommand(
                request.Email,
                request.PasswordBase64));
            return Results.Ok(result);
        });

        app.MapPost("/register", async (
            [FromBody] RegisterRequest request,
            ISender sender) =>
        {
            var result = await sender.Send(new RegisterCommand(
                request.Email,
                request.Firstname,
                request.Lastname,
                request.PasswordBase64));
            return Results.Ok(result);
        });
    }
}
