using tp_a_rendre.Domain;

namespace tp_a_rendre.Data;

public static class DbInitializer
{
    public static void Seed(LocaticDbContext context)
    {
        if (context.Cars.Any())
            return;

        var renault = new Brand("Renault", "France");
        var peugeot = new Brand("Peugeot", "France");
        context.Brands.AddRange(renault, peugeot);
        context.SaveChanges();

        var clio = new Model("Clio", renault);
        var capture = new Model("Captur", renault);
        var p208 = new Model("208", peugeot);
        context.Models.AddRange(clio, capture, p208);
        context.SaveChanges();

        context.Cars.AddRange(
            new Car("Clio 1", "AB-123-CD", 2022, 5, 15000m, "Essence", clio),
            new Car("Captur 1", "EF-456-GH", 2021, 5, 18000m, "Essence", capture),
            new Car("208 1", "IJ-789-KL", 2023, 5, 17000m, "Essence", p208)
        );

        context.Clients.AddRange(
            new Client("Sophie", "Martin", "sophie.martin@mail.fr", "0612345678"),
            new Client("Lucas", "Bernard", "lucas.bernard@mail.fr", "0698765432")
        );

        context.SaveChanges();
    }
}
