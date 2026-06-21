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
        await RemplirListesAsync(vm);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ReservationFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            await RemplirListesAsync(vm);
            return View(vm);
        }

        var reservation = new Reservation
        {
            ClientId = vm.ClientId,
            VoitureId = vm.VoitureId,
            DateDebut = vm.DateDebut,
            DateFin = vm.DateFin
        };

        var result = await _reservationService.CreateAsync(reservation);

        if (!result.Success)
        {
            vm.BusinessError = result.ErrorMessage;
            await RemplirListesAsync(vm);
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

    private async Task RemplirListesAsync(ReservationFormViewModel vm)
    {
        var clients = await _clientService.GetAllAsync();
        vm.Clients = clients.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = $"{c.Prenom} {c.Nom}"
        });

        var voitures = await _reservationService.GetVoituresAsync();
        vm.Voitures = voitures.Select(v => new SelectListItem
        {
            Value = v.Id.ToString(),
            Text = $"{v.Modele.Marque.Nom} {v.Modele.Nom} ({v.Immatriculation})"
        });
    }
}
