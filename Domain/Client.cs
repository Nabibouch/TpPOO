using System.ComponentModel.DataAnnotations;

namespace tp_a_rendre.Domain;

public class Client
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom est obligatoire.")]
    [StringLength(100)]
    [Display(Name = "Nom")]
    public string Nom { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le prénom est obligatoire.")]
    [StringLength(100)]
    [Display(Name = "Prénom")]
    public string Prenom { get; set; } = string.Empty;

    [Required(ErrorMessage = "L'email est obligatoire.")]
    [EmailAddress(ErrorMessage = "Format d'email invalide.")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le téléphone est obligatoire.")]
    [Phone(ErrorMessage = "Format de téléphone invalide.")]
    [Display(Name = "Téléphone")]
    public string Telephone { get; set; } = string.Empty;

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
