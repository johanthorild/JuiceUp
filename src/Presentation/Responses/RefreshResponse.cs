namespace Presentation.Responses;

public sealed record RefreshTokenResponse(
    Guid Id,
    string Email,
    string Refreshtoken
);