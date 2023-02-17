using Application.Dtos;

using MediatR;

namespace Application.Commands;

public sealed record CalculateRouteCommand(
    string StartLatitue,
    string StartLongitude,
    string EndLatitude,
    string EndLongitude,
    DateTime StartDatetime,
    DateTime? EndDatetime) : IRequest<CalculateRouteResult>;
