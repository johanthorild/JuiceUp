using System.Reflection;
using System.Text.Json.Serialization;

using Carter;

using Mapster;

using MapsterMapper;

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Presentation.Requests;
using Presentation.Responses;

namespace Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("juiceup", new OpenApiInfo { Title = "JuiceUp API", Version = "juiceup-v1" });
            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            config.AddSecurityDefinition("Bearer", securitySchema);

            //// TODO: Enable this later
            //var securityRequirement = new OpenApiSecurityRequirement
            //{
            //     { securitySchema, new[] { "Bearer" } }
            //};
            //config.AddSecurityRequirement(securityRequirement);

            //Set the comments path for the Swagger JSON and UI.
            var apiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, apiXmlFile), includeControllerXmlComments: true);
            var requestsXmlFile = $"{Assembly.GetAssembly(typeof(RegisterRequest))!.GetName().Name}.xml";
            config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, requestsXmlFile));
            var reponsesXmlFile = $"{Assembly.GetAssembly(typeof(LoginResponse))!.GetName().Name}.xml";
            config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, reponsesXmlFile));

            config.TagActionsBy(api =>
            {
                return api.GroupName != null
                    ? (new[] { api.GroupName })
                    : api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor
                    ? (IList<string>)(new[] { controllerActionDescriptor.ControllerName })
                    : throw new InvalidOperationException("Unable to determine tag for endpoint.");
            });
            config.DocInclusionPredicate((name, api) => true);
        });

        // Lets only use lowercase in the url's
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });

        services.AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters
                .Add(new JsonStringEnumConverter())); // Use enums as string values in models

        services.AddMappings();

        services.AddCarter();

        return services;
    }
}
