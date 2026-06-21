using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using tp_a_rendre.Domain;

namespace tp_a_rendre.ViewModels;

public class CarFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom / version de la voiture est obligatoire.")]
    [Display(Name = "Nom / Version")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "La plaque d'immatriculation est obligatoire.")]
    [StringLength(9, MinimumLength = 9, ErrorMessage = "La plaque d'immatriculation doit contenir exactement 9 caractères.")]
    [Display(Name = "Plaque d'immatriculation")]
    public string LicensePlate { get; set; } = string.Empty;

    [Required(ErrorMessage = "L'année est obligatoire.")]
    [Range(1886, 2100, ErrorMessage = "Veuillez entrer une année valide.")]
    [Display(Name = "Année")]
    public int Year { get; set; } = DateTime.Today.Year;

    [Required(ErrorMessage = "Le nombre de places est obligatoire.")]
    [Range(1, 100, ErrorMessage = "La voiture doit avoir au moins une place.")]
    [Display(Name = "Nombre de places")]
    public int SeatingCapacity { get; set; } = 5;

    [Required(ErrorMessage = "Le prix est obligatoire.")]
    [Range(0, 10000000, ErrorMessage = "Le prix doit être positif.")]
    [Display(Name = "Prix (€)")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Le type de carburant est obligatoire.")]
    [Display(Name = "Type de carburant")]
    public string FuelType { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le modèle est obligatoire.")]
    [Display(Name = "Modèle")]
    public int ModelId { get; set; }

    public IEnumerable<SelectListItem> Models { get; set; } = [];

    public string? BusinessError { get; set; }

    public static CarFormViewModel FromDomain(Car car)
    {
        return new CarFormViewModel
        {
            Id = car.Id,
            Name = car.Name,
            LicensePlate = car.LicensePlate,
            Year = car.Year,
            SeatingCapacity = car.SeatingCapacity,
            Price = car.Price,
            FuelType = car.FuelType,
            ModelId = car.ModelId
        };
    }
}
