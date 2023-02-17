using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace Application.Dtos;

/// <summary>
/// Contains response of a successfull refresh login
/// </summary>
public class RefreshTokenResult
{
    public RefreshTokenResult(DateTime expires, string userName)
    {
        Expires = expires;
        UserName = userName;
        Token = GenerateRefreshTokenString();
    }

    private static string GenerateRefreshTokenString()
    {
        var randomNumber = new byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public string Token { get; init; }

    public DateTime Expires { get; init; }

    [JsonIgnore]
    public string UserName { get; init; }
}
