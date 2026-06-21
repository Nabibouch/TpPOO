using tp_a_rendre.Domain;

namespace tp_a_rendre.Services;

public interface IClientService
{
    Task<IReadOnlyList<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(int id);
    Task<ServiceResult<Client>> CreateAsync(string firstName, string lastName, string email, string phoneNumber);
    Task<ServiceResult<Client>> UpdateAsync(int id, string firstName, string lastName, string email, string phoneNumber);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
