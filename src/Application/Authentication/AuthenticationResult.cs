namespace Application.Authentication;

/// <summary>
/// Contains response of a successfull login
/// </summary>
public record AuthenticationResult(
    Guid Id,
    string Email,
    string Token
);
