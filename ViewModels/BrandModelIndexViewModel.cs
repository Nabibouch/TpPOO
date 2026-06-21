using tp_a_rendre.Domain;

namespace tp_a_rendre.ViewModels;

public class BrandModelIndexViewModel
{
    public IReadOnlyList<Brand> Brands { get; set; } = [];
    public IReadOnlyList<Model> Models { get; set; } = [];
}
