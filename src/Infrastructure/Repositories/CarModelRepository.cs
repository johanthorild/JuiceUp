using Domain.Entities;
using Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public sealed class CarModelRepository : ICarModelRepository
{
    private readonly AppDbContext _dbContext;

    public CarModelRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CarModel?> GetById(int id)
    {
        var carModel = await _dbContext.CarModels
            .FirstOrDefaultAsync(x => x.Id == id);

        return carModel ?? null;
    }

    public async Task<IEnumerable<CarModel>> GetAll()
    {
        var carModels = await _dbContext.CarModels
            .ToArrayAsync();

        return carModels ?? Array.Empty<CarModel>();
    }

    public void Insert(CarModel carModel)
    {
        _dbContext?.Set<CarModel>().Add(carModel);
    }

    public void Update(CarModel carModel)
    {
        _dbContext?.Set<CarModel>().Update(carModel);
    }

    public void Delete(int id)
    {
        var carModel = _dbContext?.Set<CarModel>()
            .Include(u => u.UserCars)
            .FirstOrDefault(x => x.Id == id);

        if (carModel is not null)
        {
            _dbContext?.Set<UserCar>().RemoveRange(carModel.UserCars);
        }
    }
}
