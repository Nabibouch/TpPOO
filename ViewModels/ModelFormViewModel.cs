using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using tp_a_rendre.Domain;

namespace tp_a_rendre.ViewModels;

public class ModelFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom du modèle est obligatoire.")]
    [Display(Name = "Nom du modèle")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le choix d'une marque est obligatoire.")]
    [Display(Name = "Marque")]
    public int BrandId { get; set; }

    public IEnumerable<SelectListItem> Brands { get; set; } = [];

    public string? BusinessError { get; set; }

    public static ModelFormViewModel FromDomain(Model model)
    {
        return new ModelFormViewModel
        {
            Id = model.Id,
            Name = model.Name,
            BrandId = model.BrandId
        };
    }
}
