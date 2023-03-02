namespace Presentation.Requests;
internal record UpdateCarModelRequest(
    int Id,
    string Name,
    double Capacity,
    double RealRange);
