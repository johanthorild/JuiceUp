using Domain.Entities;

using MediatR;

namespace Application.Queries;
public sealed record GetStationsQuery() : IRequest<IEnumerable<Station>>;