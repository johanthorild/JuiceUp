using Application.Dtos;

using MediatR;

namespace Application.Commands;

public sealed record RefreshCommand(
    string Accesstoken,
    string Refreshtoken) : IRequest<RefreshTokenResult>;
