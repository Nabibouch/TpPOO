using tp_a_rendre.Domain;

namespace tp_a_rendre.Services;

public interface IModelService
{
    Task<IReadOnlyList<Model>> GetAllAsync();
    Task<Model?> GetByIdAsync(int id);
    Task<ServiceResult<Model>> CreateAsync(string name, int brandId);
}
