using Domain.Entities;

namespace Domain.Repositories;

public interface ICarModelRepository
{
    Task<CarModel?> GetById(int id);
    Task<IEnumerable<CarModel>> GetAll();
    void Insert(CarModel station);
    void Update(CarModel station);
    void Delete(int id);
}