using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace tp_a_rendre.ViewModels;

public class ReservationFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Veuillez sélectionner un client.")]
    [Display(Name = "Client")]
    public int ClientId { get; set; }

    [Required(ErrorMessage = "Veuillez sélectionner une voiture.")]
    [Display(Name = "Voiture")]
    public int CarId { get; set; }

    [Required(ErrorMessage = "La date de début est obligatoire.")]
    [Display(Name = "Date de début")]
    [DataType(DataType.Date)]
    public DateTime StartOn { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "La date de fin est obligatoire.")]
    [Display(Name = "Date de fin")]
    [DataType(DataType.Date)]
    public DateTime EndOn { get; set; } = DateTime.Today.AddDays(1);

    public IEnumerable<SelectListItem> Clients { get; set; } = [];
    public IEnumerable<SelectListItem> Cars { get; set; } = [];

    public string? BusinessError { get; set; }
}
