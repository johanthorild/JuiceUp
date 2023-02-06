namespace Presentation.Responses;

public sealed record UserCarResponse(
    int Id,
    string Name,
    string CarModelName,
    double Capacity
);