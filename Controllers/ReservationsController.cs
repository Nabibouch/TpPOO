using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using tp_a_rendre.Domain;
using tp_a_rendre.Services;
using tp_a_rendre.ViewModels;

namespace tp_a_rendre.Controllers;

public class ReservationsController : Controller
{
    private readonly IReservationService _reservationService;
    private readonly IClientService _clientService;

    public ReservationsController(
        IReservationService reservationService,
        IClientService clientService)
    {
        _reservationService = reservationService;
        _clientService = clientService;
    }

    public async Task<IActionResult> Index()
    {
        var reservations = await _reservationService.GetAllAsync();
        return View(reservations);
    }

    public async Task<IActionResult> Create()
    {
        var vm = new ReservationFormViewModel();
        await FillListsAsync(vm);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ReservationFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            await FillListsAsync(vm);
            return View(vm);
        }

        var result = await _reservationService.CreateAsync(
            vm.ClientId,
            vm.CarId,
            vm.StartOn,
            vm.EndOn);

        if (!result.Success)
        {
            vm.BusinessError = result.ErrorMessage;
            await FillListsAsync(vm);
            return View(vm);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var reservation = await _reservationService.GetByIdAsync(id);
        if (reservation is null)
            return NotFound();

        return View(reservation);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _reservationService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task FillListsAsync(ReservationFormViewModel vm)
    {
        var clients = await _clientService.GetAllAsync();
        vm.Clients = clients.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = $"{c.FirstName} {c.LastName}"
        });

        var cars = await _reservationService.GetCarsAsync();
        vm.Cars = cars.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = $"{c.Model.Brand.Name} {c.Model.Name} ({c.LicensePlate})"
        });
    }
}
