namespace Presentation.Responses;

public sealed record RefreshResponse(
    Guid Id,
    string Email,
    string Refreshtoken
);