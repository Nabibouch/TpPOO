using Microsoft.AspNetCore.Mvc;
using tp_a_rendre.Domain;
using tp_a_rendre.Services;

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
        return View(Client.CreateForForm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Client client)
    {
        if (!ModelState.IsValid)
            return View(client);

        await _clientService.CreateAsync(client);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client is null)
            return NotFound();

        return View(client);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Client client)
    {
        if (id != client.Id)
            return NotFound();

        if (!ModelState.IsValid)
            return View(client);

        await _clientService.UpdateAsync(client);
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
