using Domain.Entities;
using Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public sealed class StationRepository : IStationRepository
{
    private readonly AppDbContext _dbContext;

    public StationRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Station?> GetById(int id)
    {
        var station = await _dbContext.Stations
            .Include(u => u.Chargers)
                .ThenInclude(u => u.ChargerSpeed)
            .FirstOrDefaultAsync(x => x.Id == id);

        return station ?? null;
    }

    public async Task<IEnumerable<Station>> GetAll()
    {
        var stations = await _dbContext.Stations
            //.Include(u => u.Chargers)
            //.ThenInclude(u => u.ChargerSpeed)
            .ToArrayAsync();

        return stations ?? Array.Empty<Station>();
    }

    public void Insert(Station station)
    {
        _dbContext?.Set<Station>().Add(station);
    }

    public void Update(Station station)
    {
        _dbContext?.Set<Station>().Update(station);
    }

    public void Delete(int id)
    {
        var station = _dbContext?.Set<Station>()
            .Include(u => u.Chargers)
                .ThenInclude(u => u.Reservations)
            .FirstOrDefault(x => x.Id == id);

        if (station is not null)
        {
            _dbContext?.Set<Charger>().RemoveRange(station.Chargers);
            _dbContext?.Set<Reservation>().RemoveRange(station.Chargers.SelectMany(c => c.Reservations));
            _dbContext?.Set<Station>().Remove(station);
        }
    }
}
