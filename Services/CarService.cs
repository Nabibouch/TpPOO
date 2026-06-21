using Microsoft.EntityFrameworkCore;
using tp_a_rendre.Data;
using tp_a_rendre.Domain;

namespace tp_a_rendre.Services;

public class CarService : ICarService
{
    private readonly LocaticDbContext _context;

    public CarService(LocaticDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Car>> GetAllAsync()
    {
        return await _context.Cars
            .Include(c => c.Model)
                .ThenInclude(m => m.Brand)
            .OrderBy(c => c.Model.Brand.Name)
            .ThenBy(c => c.Model.Name)
            .ThenBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<Car?> GetByIdAsync(int id)
    {
        var car = await _context.Cars
            .Include(c => c.Model)
                .ThenInclude(m => m.Brand)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (car is not null)
        {
            var reservations = await _context.Reservations
                .Where(r => r.CarId == id)
                .Include(r => r.Client)
                .ToListAsync();

            foreach (var res in reservations)
            {
                if (!car.Reservations.Any(r => r.Id == res.Id))
                {
                    car.AddReservation(res);
                }
            }
        }

        return car;
    }

    public async Task<ServiceResult<Car>> CreateAsync(string name, string licensePlate, int year, int seatingCapacity, decimal price, string fuelType, int modelId)
    {
        var model = await _context.Models
            .Include(m => m.Brand)
            .FirstOrDefaultAsync(m => m.Id == modelId);

        if (model is null)
        {
            return ServiceResult<Car>.Fail("Modèle introuvable.");
        }

        try
        {
            var car = new Car(name, licensePlate, year, seatingCapacity, price, fuelType, model);
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return ServiceResult<Car>.Ok(car);
        }
        catch (ArgumentException ex)
        {
            return ServiceResult<Car>.Fail(ex.Message);
        }
    }

    public async Task<ServiceResult<Car>> UpdateAsync(int id, string name, string licensePlate, int year, int seatingCapacity, decimal price, string fuelType, int modelId)
    {
        var car = await _context.Cars
            .Include(c => c.Model)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (car is null)
        {
            return ServiceResult<Car>.Fail("Voiture introuvable.");
        }

        var model = await _context.Models
            .Include(m => m.Brand)
            .FirstOrDefaultAsync(m => m.Id == modelId);

        if (model is null)
        {
            return ServiceResult<Car>.Fail("Modèle introuvable.");
        }

        try
        {
            car.Name = name;
            car.LicensePlate = licensePlate;
            car.Year = year;
            car.SeatingCapacity = seatingCapacity;
            car.Price = price;
            car.FuelType = fuelType;
            car.Model = model;

            await _context.SaveChangesAsync();
            return ServiceResult<Car>.Ok(car);
        }
        catch (ArgumentException ex)
        {
            return ServiceResult<Car>.Fail(ex.Message);
        }
    }

    public async Task DeleteAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car is null)
            return;

        var reservations = await _context.Reservations.Where(r => r.CarId == id).ToListAsync();
        _context.Reservations.RemoveRange(reservations);

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Cars.AnyAsync(c => c.Id == id);
    }
}
