using tp_a_rendre.Domain;

namespace tp_a_rendre.Services;

public interface IReservationService
{
    Task<IReadOnlyList<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync(int id);
    Task<IReadOnlyList<Car>> GetCarsAsync();
    Task<ServiceResult<Reservation>> CreateAsync(int clientId, int carId, DateTime startOn, DateTime endOn);
    Task DeleteAsync(int id);
}
