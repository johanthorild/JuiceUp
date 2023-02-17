using Domain.Entities;

namespace Domain.Repositories;

public interface IStationRepository
{
    Task<Station?> GetById(int id);
    Task<IEnumerable<Station>> GetAll();
    void Insert(Station station);
    void Update(Station station);
    void Delete(int id);
}

