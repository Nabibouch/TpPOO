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

    public async Task<ServiceResult<Client>> CreateAsync(string firstName, string lastName, string email, string phoneNumber)
    {
        try
        {
            var client = new Client(firstName, lastName, email, phoneNumber);
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return ServiceResult<Client>.Ok(client);
        }
        catch (ArgumentException ex)
        {
            return ServiceResult<Client>.Fail(ex.Message);
        }
    }

    public async Task<ServiceResult<Client>> UpdateAsync(int id, string firstName, string lastName, string email, string phoneNumber)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client is null)
        {
            return ServiceResult<Client>.Fail("Client introuvable.");
        }

        try
        {
            client.FirstName = firstName;
            client.LastName = lastName;
            client.Email = email;
            client.PhoneNumber = phoneNumber;
            await _context.SaveChangesAsync();
            return ServiceResult<Client>.Ok(client);
        }
        catch (ArgumentException ex)
        {
            return ServiceResult<Client>.Fail(ex.Message);
        }
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
