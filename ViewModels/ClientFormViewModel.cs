using System.ComponentModel.DataAnnotations;

namespace tp_a_rendre.ViewModels;

public class ClientFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le prénom est obligatoire.")]
    [Display(Name = "Prénom")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le nom est obligatoire.")]
    [Display(Name = "Nom")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "L'email est obligatoire.")]
    [EmailAddress(ErrorMessage = "Format d'email invalide.")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le téléphone est obligatoire.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Le téléphone doit contenir 10 chiffres.")]
    [Display(Name = "Téléphone")]
    public string PhoneNumber { get; set; } = string.Empty;

    public string? BusinessError { get; set; }

    public static ClientFormViewModel FromDomain(Domain.Client client)
    {
        return new ClientFormViewModel
        {
            Id = client.Id,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber
        };
    }
}
