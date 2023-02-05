using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Options;

public class JwtOptions
{
    public const string OptionsKey = "Jwt";

    [Required]
    public string Secret { get; init; } = null!;

    [Range(1, 24 * 60)]
    public int AccessTokenExpirationTimeInMinutes { get; init; } = 10;

    [Range(1, 31 * 24 * 60)]
    public int RefreshTokenExpirationTimeInMinutes { get; init; } = 600;

    [Range(1, 24 * 60)]
    public int PasswordResetExpirationTimeInMinutes { get; init; } = 10;

    [JsonIgnore]
    public TimeSpan PasswordResetExpirationTime => TimeSpan.FromMinutes(PasswordResetExpirationTimeInMinutes);

    [JsonIgnore]
    public TokenValidationParameters? TokenValidationParameters { get; set; }
}
