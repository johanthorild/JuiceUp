namespace Application.Dtos;

/// <summary>
/// Contains response of a successfull login
/// </summary>
public record LoginResult(
    Guid Id,
    string Email,
    string Accesstoken,
    RefreshTokenResult Refreshtoken
);
