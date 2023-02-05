namespace Application;

/// <summary>
/// Contains response of a successfull login
/// </summary>
public record LoginResult(
    Guid Id,
    string Email,
    string Accesstoken
);
