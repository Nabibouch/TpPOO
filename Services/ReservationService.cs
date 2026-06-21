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
            .Include(r => r.Voiture)
                .ThenInclude(v => v.Modele)
                    .ThenInclude(m => m.Marque)
            .OrderByDescending(r => r.DateDebut)
            .ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        return await _context.Reservations
            .Include(r => r.Client)
            .Include(r => r.Voiture)
                .ThenInclude(v => v.Modele)
                    .ThenInclude(m => m.Marque)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IReadOnlyList<Voiture>> GetVoituresAsync()
    {
        return await _context.Voitures
            .Include(v => v.Modele)
                .ThenInclude(m => m.Marque)
            .OrderBy(v => v.Modele.Marque.Nom)
            .ThenBy(v => v.Modele.Nom)
            .ThenBy(v => v.Immatriculation)
            .ToListAsync();
    }

    public async Task<ServiceResult<Reservation>> CreateAsync(Reservation reservation)
    {
        if (reservation.DateFin < reservation.DateDebut)
        {
            return ServiceResult<Reservation>.Fail(
                "La date de fin doit être postérieure ou égale à la date de début.");
        }

        var chevauchement = await _context.Reservations.AnyAsync(r =>
            r.VoitureId == reservation.VoitureId &&
            reservation.DateDebut <= r.DateFin &&
            reservation.DateFin >= r.DateDebut);

        if (chevauchement)
        {
            return ServiceResult<Reservation>.Fail(
                "Cette voiture est déjà réservée sur tout ou partie de la période choisie.");
        }

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        return ServiceResult<Reservation>.Ok(reservation);
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
