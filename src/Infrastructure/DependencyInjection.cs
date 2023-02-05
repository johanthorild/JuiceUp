using System.Text;

using Application.Providers;

using Domain;
using Domain.Repositories;

using Infrastructure.Options;
using Infrastructure.Providers;
using Infrastructure.Repositories;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
            .AddPersistance(configuration)
            .AddAuthenticationAndAuthorization(configuration);

        services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.OptionsKey));
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    public static IServiceCollection AddPersistance(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MainDb")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    public static IServiceCollection AddAuthenticationAndAuthorization(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtOptions = ConfigureOptions<JwtOptions>(services, configuration, JwtOptions.OptionsKey);
        var addressOptions = ConfigureOptions<AddressOptions>(services, configuration, AddressOptions.OptionsKey);

        services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();
        services.AddTransient<IHttpContextUserProvider, HttpContextUserProvider>();

        services.AddHttpContextAccessor();

        services.AddAuthorization();

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
            options.TokenValidationParameters = CreateTokenValidationParameters(
                jwtOptions.Secret,
                addressOptions.SiteUrl));

        // Add (post configuration) token validation parameters to the refresh token checks (not the same instance of options as above)
        services.PostConfigure<JwtOptions>(options =>
        {
            options.TokenValidationParameters = CreateTokenValidationParameters(
                jwtOptions.Secret,
                addressOptions.SiteUrl);
            options.TokenValidationParameters.ValidateLifetime = false;
        });

        return services;
    }

    static TOptions ConfigureOptions<TOptions>(
        IServiceCollection services,
        IConfiguration configuration,
        string optionsKey) where TOptions : class, new()
    {
        var configSection = configuration.GetSection(optionsKey);

        services.AddOptions<TOptions>()
            .Bind(configSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var instance = configSection.Get<TOptions>();
        return instance is null ? new TOptions() : instance;
    }

    static TokenValidationParameters CreateTokenValidationParameters(string jwtSecret, string siteUrl)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = siteUrl,
            ValidAudience = siteUrl,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSecret))
        };
    }
}
