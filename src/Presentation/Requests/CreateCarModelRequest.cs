namespace Presentation.Requests;
internal sealed record CreateCarModelRequest(
    string Name,
    double Capacity,
    double RealRange);
