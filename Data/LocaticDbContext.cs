using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using tp_a_rendre.Domain;

namespace tp_a_rendre.Data;

public class LocaticDbContext : DbContext
{
    public LocaticDbContext(DbContextOptions<LocaticDbContext> options) : base(options)
    {
    }

    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Model> Models => Set<Model>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>().Ignore(b => b.Models);

        modelBuilder.Entity<Model>(entity =>
        {
            entity.Property(m => m.BrandId)
                .HasField("_brandId")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            entity.Navigation(m => m.Brand)
                .HasField("_brand")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            entity.HasOne(m => m.Brand)
                .WithMany()
                .HasForeignKey(m => m.BrandId);
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.Ignore(c => c.Reservations);
            entity.Property(c => c.ModelId)
                .HasField("_modelId")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            entity.Navigation(c => c.Model)
                .HasField("_model")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            entity.HasOne(c => c.Model)
                .WithMany()
                .HasForeignKey(c => c.ModelId);
        });

        modelBuilder.Entity<Client>().Ignore(c => c.Reservations);

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.Property(r => r.CarId)
                .HasField("_carId")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            entity.Property(r => r.ClientId)
                .HasField("_clientId")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            entity.Navigation(r => r.Car)
                .HasField("_car")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            entity.Navigation(r => r.Client)
                .HasField("_client")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            entity.HasOne(r => r.Client)
                .WithMany()
                .HasForeignKey(r => r.ClientId);
            entity.HasOne(r => r.Car)
                .WithMany()
                .HasForeignKey(r => r.CarId);
        });
    }
}
