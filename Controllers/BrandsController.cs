using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using tp_a_rendre.Services;
using tp_a_rendre.ViewModels;

namespace tp_a_rendre.Controllers;

public class BrandsController : Controller
{
    private readonly IBrandService _brandService;
    private readonly IModelService _modelService;

    public BrandsController(IBrandService brandService, IModelService modelService)
    {
        _brandService = brandService;
        _modelService = modelService;
    }

    public async Task<IActionResult> Index()
    {
        var brands = await _brandService.GetAllAsync();
        var models = await _modelService.GetAllAsync();

        var vm = new BrandModelIndexViewModel
        {
            Brands = brands,
            Models = models
        };

        return View(vm);
    }

    public IActionResult Create()
    {
        return View(new BrandFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BrandFormViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var result = await _brandService.CreateAsync(vm.Name, vm.Origin);

        if (!result.Success)
        {
            vm.BusinessError = result.ErrorMessage;
            return View(vm);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> CreateModel()
    {
        var vm = new ModelFormViewModel();
        await FillBrandsListAsync(vm);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateModel(ModelFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            await FillBrandsListAsync(vm);
            return View(vm);
        }

        var result = await _modelService.CreateAsync(vm.Name, vm.BrandId);

        if (!result.Success)
        {
            vm.BusinessError = result.ErrorMessage;
            await FillBrandsListAsync(vm);
            return View(vm);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task FillBrandsListAsync(ModelFormViewModel vm)
    {
        var brands = await _brandService.GetAllAsync();
        vm.Brands = brands.Select(b => new SelectListItem
        {
            Value = b.Id.ToString(),
            Text = b.Name
        });
    }
}
