namespace Presentation.Responses;
public sealed record CarModelResponse(
    int Id,
    string Name,
    double Capacity,
    double RealRange,
    DateTime? LastChanged,
    string? LastChangedBy
);