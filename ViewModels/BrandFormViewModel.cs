using System.ComponentModel.DataAnnotations;
using tp_a_rendre.Domain;

namespace tp_a_rendre.ViewModels;

public class BrandFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom de la marque est obligatoire.")]
    [Display(Name = "Nom de la marque")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le pays d'origine est obligatoire.")]
    [Display(Name = "Pays d'origine")]
    public string Origin { get; set; } = string.Empty;

    public string? BusinessError { get; set; }

    public static BrandFormViewModel FromDomain(Brand brand)
    {
        return new BrandFormViewModel
        {
            Id = brand.Id,
            Name = brand.Name,
            Origin = brand.Origin
        };
    }
}
