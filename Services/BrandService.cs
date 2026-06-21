using Microsoft.EntityFrameworkCore;
using tp_a_rendre.Data;
using tp_a_rendre.Domain;

namespace tp_a_rendre.Services;

public class BrandService : IBrandService
{
    private readonly LocaticDbContext _context;

    public BrandService(LocaticDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Brand>> GetAllAsync()
    {
        return await _context.Brands
            .OrderBy(b => b.Name)
            .ToListAsync();
    }

    public async Task<Brand?> GetByIdAsync(int id)
    {
        return await _context.Brands.FindAsync(id);
    }

    public async Task<ServiceResult<Brand>> CreateAsync(string name, string origin)
    {
        try
        {
            var brand = new Brand(name, origin);
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return ServiceResult<Brand>.Ok(brand);
        }
        catch (ArgumentException ex)
        {
            return ServiceResult<Brand>.Fail(ex.Message);
        }
    }
}
