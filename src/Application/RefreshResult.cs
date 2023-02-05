namespace Application;

/// <summary>
/// Contains response of a successfull refresh login
/// </summary>
public record RefreshResult(
    Guid Id,
    string Email,
    string Refreshtoken
);
