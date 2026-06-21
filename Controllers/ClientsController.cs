using Microsoft.AspNetCore.Mvc;
using tp_a_rendre.Services;
using tp_a_rendre.ViewModels;

namespace tp_a_rendre.Controllers;

public class ClientsController : Controller
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<IActionResult> Index()
    {
        var clients = await _clientService.GetAllAsync();
        return View(clients);
    }

    public IActionResult Create()
    {
        return View(new ClientFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClientFormViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var result = await _clientService.CreateAsync(vm.FirstName, vm.LastName, vm.Email, vm.PhoneNumber);

        if (!result.Success)
        {
            vm.BusinessError = result.ErrorMessage;
            return View(vm);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client is null)
            return NotFound();

        return View(ClientFormViewModel.FromDomain(client));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ClientFormViewModel vm)
    {
        if (id != vm.Id)
            return NotFound();

        if (!ModelState.IsValid)
            return View(vm);

        var result = await _clientService.UpdateAsync(id, vm.FirstName, vm.LastName, vm.Email, vm.PhoneNumber);

        if (!result.Success)
        {
            vm.BusinessError = result.ErrorMessage;
            return View(vm);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client is null)
            return NotFound();

        return View(client);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _clientService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
