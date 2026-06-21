namespace tp_a_rendre.Domain;

public class Voiture
{
    public int Id { get; set; }
    public string Immatriculation { get; set; } = string.Empty;
    public int ModeleId { get; set; }

    public Modele Modele { get; set; } = null!;
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
