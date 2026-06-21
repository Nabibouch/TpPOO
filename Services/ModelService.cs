using Microsoft.EntityFrameworkCore;
using tp_a_rendre.Data;
using tp_a_rendre.Domain;

namespace tp_a_rendre.Services;

public class ModelService : IModelService
{
    private readonly LocaticDbContext _context;

    public ModelService(LocaticDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Model>> GetAllAsync()
    {
        return await _context.Models
            .Include(m => m.Brand)
            .OrderBy(m => m.Brand.Name)
            .ThenBy(m => m.Name)
            .ToListAsync();
    }

    public async Task<Model?> GetByIdAsync(int id)
    {
        return await _context.Models
            .Include(m => m.Brand)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<ServiceResult<Model>> CreateAsync(string name, int brandId)
    {
        var brand = await _context.Brands.FindAsync(brandId);
        if (brand is null)
        {
            return ServiceResult<Model>.Fail("Marque introuvable.");
        }

        try
        {
            var model = new Model(name, brand);
            _context.Models.Add(model);
            await _context.SaveChangesAsync();
            return ServiceResult<Model>.Ok(model);
        }
        catch (ArgumentException ex)
        {
            return ServiceResult<Model>.Fail(ex.Message);
        }
    }
}
