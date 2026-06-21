using tp_a_rendre.Domain;

namespace tp_a_rendre.Services;

public interface IClientService
{
    Task<IReadOnlyList<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(int id);
    Task<Client> CreateAsync(Client client);
    Task UpdateAsync(Client client);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
