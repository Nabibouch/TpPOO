namespace tp_a_rendre.Domain;

public class Reservation
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int VoitureId { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }

    public Client Client { get; set; } = null!;
    public Voiture Voiture { get; set; } = null!;
}
