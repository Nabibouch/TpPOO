using Microsoft.EntityFrameworkCore;
using tp_a_rendre.Domain;

namespace tp_a_rendre.Data;

public class LocaticDbContext : DbContext
{
    public LocaticDbContext(DbContextOptions<LocaticDbContext> options) : base(options)
    {
    }

    public DbSet<Marque> Marques => Set<Marque>();
    public DbSet<Modele> Modeles => Set<Modele>();
    public DbSet<Voiture> Voitures => Set<Voiture>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Modele>()
            .HasOne(m => m.Marque)
            .WithMany(mq => mq.Modeles)
            .HasForeignKey(m => m.MarqueId);

        modelBuilder.Entity<Voiture>()
            .HasOne(v => v.Modele)
            .WithMany(m => m.Voitures)
            .HasForeignKey(v => v.ModeleId);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Client)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.ClientId);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Voiture)
            .WithMany(v => v.Reservations)
            .HasForeignKey(r => r.VoitureId);
    }
}
