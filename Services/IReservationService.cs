using tp_a_rendre.Domain;

namespace tp_a_rendre.Services;

public interface IReservationService
{
    Task<IReadOnlyList<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync(int id);
    Task<IReadOnlyList<Voiture>> GetVoituresAsync();
    Task<ServiceResult<Reservation>> CreateAsync(Reservation reservation);
    Task DeleteAsync(int id);
}
