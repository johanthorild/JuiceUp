using Domain.Entities;

using MediatR;

namespace Application.Queries;
public sealed record GetChargersQuery() : IRequest<IEnumerable<Charger>>;