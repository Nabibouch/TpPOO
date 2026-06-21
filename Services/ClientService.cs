using Microsoft.EntityFrameworkCore;
using tp_a_rendre.Data;
using tp_a_rendre.Domain;

namespace tp_a_rendre.Services;

public class ClientService : IClientService
{
    private readonly LocaticDbContext _context;

    public ClientService(LocaticDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Client>> GetAllAsync()
    {
        return await _context.Clients
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<Client> CreateAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task UpdateAsync(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client is null)
            return;

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Clients.AnyAsync(c => c.Id == id);
    }
}
