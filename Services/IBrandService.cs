using tp_a_rendre.Domain;

namespace tp_a_rendre.Services;

public interface IBrandService
{
    Task<IReadOnlyList<Brand>> GetAllAsync();
    Task<Brand?> GetByIdAsync(int id);
    Task<ServiceResult<Brand>> CreateAsync(string name, string origin);
}
