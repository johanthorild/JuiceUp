namespace Presentation.Responses;

public sealed record LoginResponse(
    Guid Id,
    string Email,
    string Accesstoken
);