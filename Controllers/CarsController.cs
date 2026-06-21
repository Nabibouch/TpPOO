using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using tp_a_rendre.Services;
using tp_a_rendre.ViewModels;

namespace tp_a_rendre.Controllers;

public class CarsController : Controller
{
    private readonly ICarService _carService;
    private readonly IModelService _modelService;

    public CarsController(ICarService carService, IModelService modelService)
    {
        _carService = carService;
        _modelService = modelService;
    }

    public async Task<IActionResult> Index()
    {
        var cars = await _carService.GetAllAsync();
        return View(cars);
    }

    public async Task<IActionResult> Details(int id)
    {
        var car = await _carService.GetByIdAsync(id);
        if (car is null)
            return NotFound();

        return View(car);
    }

    public async Task<IActionResult> Create()
    {
        var vm = new CarFormViewModel();
        await FillModelsListAsync(vm);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CarFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            await FillModelsListAsync(vm);
            return View(vm);
        }

        var result = await _carService.CreateAsync(
            vm.Name,
            vm.LicensePlate,
            vm.Year,
            vm.SeatingCapacity,
            vm.Price,
            vm.FuelType,
            vm.ModelId);

        if (!result.Success)
        {
            vm.BusinessError = result.ErrorMessage;
            await FillModelsListAsync(vm);
            return View(vm);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var car = await _carService.GetByIdAsync(id);
        if (car is null)
            return NotFound();

        var vm = CarFormViewModel.FromDomain(car);
        await FillModelsListAsync(vm);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CarFormViewModel vm)
    {
        if (id != vm.Id)
            return NotFound();

        if (!ModelState.IsValid)
        {
            await FillModelsListAsync(vm);
            return View(vm);
        }

        var result = await _carService.UpdateAsync(
            id,
            vm.Name,
            vm.LicensePlate,
            vm.Year,
            vm.SeatingCapacity,
            vm.Price,
            vm.FuelType,
            vm.ModelId);

        if (!result.Success)
        {
            vm.BusinessError = result.ErrorMessage;
            await FillModelsListAsync(vm);
            return View(vm);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var car = await _carService.GetByIdAsync(id);
        if (car is null)
            return NotFound();

        return View(car);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _carService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task FillModelsListAsync(CarFormViewModel vm)
    {
        var models = await _modelService.GetAllAsync();
        vm.Models = models.Select(m => new SelectListItem
        {
            Value = m.Id.ToString(),
            Text = $"{m.Brand.Name} {m.Name}"
        });
    }
}
