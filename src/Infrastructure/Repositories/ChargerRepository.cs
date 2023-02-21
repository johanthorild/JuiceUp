using Domain.Entities;
using Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public sealed class ChargerRepository : IChargerRepository
{
    private readonly AppDbContext _dbContext;

    public ChargerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Charger?> GetById(int id)
    {
        var chargers = await _dbContext.Chargers
            .Include(u => u.ChargerSpeed)
            .Include(u => u.Reservations)
            .FirstOrDefaultAsync(x => x.Id == id);

        return chargers ?? null;
    }

    public async Task<IEnumerable<Charger>> GetAll()
    {
        var chargers = await _dbContext.Chargers
            .Include(u => u.ChargerSpeed)
            .Include(u => u.Reservations)
            .ToArrayAsync();

        return chargers ?? Array.Empty<Charger>();
    }

    public async Task<IEnumerable<ChargerSpeed>> GetAllSpeeds()
    {
        var speeds = await _dbContext.ChargerSpeeds.ToArrayAsync();

        return speeds ?? Array.Empty<ChargerSpeed>();
    }

    public void Insert(Charger charger)
    {
        _dbContext?.Set<Charger>().Add(charger);
    }

    public void Update(Charger charger)
    {
        _dbContext?.Set<Charger>().Update(charger);
    }

    public void Delete(int id)
    {
        var charger = _dbContext?.Set<Charger>()
            .Include(u => u.Reservations)
            .FirstOrDefault(x => x.Id == id);

        if (charger is not null)
        {
            _dbContext?.Set<Reservation>().RemoveRange(charger.Reservations);
            _dbContext?.Set<Charger>().Remove(charger);
        }
    }
}
