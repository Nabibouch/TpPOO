namespace tp_a_rendre.Domain;

public class Marque
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;

    public ICollection<Modele> Modeles { get; set; } = new List<Modele>();
}
