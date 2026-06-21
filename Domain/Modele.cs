namespace tp_a_rendre.Domain;

public class Modele
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public int MarqueId { get; set; }

    public Marque Marque { get; set; } = null!;
    public ICollection<Voiture> Voitures { get; set; } = new List<Voiture>();
}
