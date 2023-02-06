using Domain.Entities;

using MediatR;

namespace Application.Queries;
public sealed record GetUserByIdQuery(
    Guid Id) : IRequest<User>;