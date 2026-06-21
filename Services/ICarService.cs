using tp_a_rendre.Domain;

namespace tp_a_rendre.Services;

public interface ICarService
{
    Task<IReadOnlyList<Car>> GetAllAsync();
    Task<Car?> GetByIdAsync(int id);
    Task<ServiceResult<Car>> CreateAsync(string name, string licensePlate, int year, int seatingCapacity, decimal price, string fuelType, int modelId);
    Task<ServiceResult<Car>> UpdateAsync(int id, string name, string licensePlate, int year, int seatingCapacity, decimal price, string fuelType, int modelId);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
