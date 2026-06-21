using Microsoft.EntityFrameworkCore;
using tp_a_rendre.Data;
using tp_a_rendre.Domain;

namespace tp_a_rendre.Services;

public class ReservationService : IReservationService
{
    private readonly LocaticDbContext _context;

    public ReservationService(LocaticDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Reservation>> GetAllAsync()
    {
        return await _context.Reservations
            .Include(r => r.Client)
            .Include(r => r.Car)
                .ThenInclude(c => c.Model)
                    .ThenInclude(m => m.Brand)
            .OrderByDescending(r => r.StartOn)
            .ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        return await _context.Reservations
            .Include(r => r.Client)
            .Include(r => r.Car)
                .ThenInclude(c => c.Model)
                    .ThenInclude(m => m.Brand)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IReadOnlyList<Car>> GetCarsAsync()
    {
        return await _context.Cars
            .Include(c => c.Model)
                .ThenInclude(m => m.Brand)
            .OrderBy(c => c.Model.Brand.Name)
            .ThenBy(c => c.Model.Name)
            .ThenBy(c => c.LicensePlate)
            .ToListAsync();
    }

    public async Task<ServiceResult<Reservation>> CreateAsync(int clientId, int carId, DateTime startOn, DateTime endOn)
    {
        var client = await _context.Clients.FindAsync(clientId);
        if (client is null)
        {
            return ServiceResult<Reservation>.Fail("Client introuvable.");
        }

        var car = await _context.Cars.FindAsync(carId);
        if (car is null)
        {
            return ServiceResult<Reservation>.Fail("Voiture introuvable.");
        }

        if (endOn < startOn)
        {
            return ServiceResult<Reservation>.Fail(
                "La date de fin doit être postérieure ou égale à la date de début.");
        }

        var chevauchement = await _context.Reservations.AnyAsync(r =>
            r.CarId == carId &&
            startOn <= r.EndOn &&
            endOn >= r.StartOn);

        if (chevauchement)
        {
            return ServiceResult<Reservation>.Fail(
                "Cette voiture est déjà réservée sur tout ou partie de la période choisie.");
        }

        try
        {
            var reservation = new Reservation(startOn, endOn, car, client);
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return ServiceResult<Reservation>.Ok(reservation);
        }
        catch (ArgumentException ex)
        {
            return ServiceResult<Reservation>.Fail(ex.Message);
        }
    }

    public async Task DeleteAsync(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation is null)
            return;

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
    }
}
