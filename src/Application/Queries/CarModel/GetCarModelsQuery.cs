using Domain.Entities;

using MediatR;

namespace Application.Queries;
public sealed record GetCarModelsQuery() : IRequest<IEnumerable<CarModel>>;