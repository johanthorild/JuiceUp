using Domain.Entities;

namespace Domain.Repositories;

public interface IChargerRepository
{
    Task<Charger?> GetById(int id);
    Task<IEnumerable<Charger>> GetAll();
    Task<IEnumerable<ChargerSpeed>> GetAllSpeeds();
    void Insert(Charger charger);
    void Update(Charger charger);
    void Delete(int id);
}