using tp_a_rendre.Domain;

namespace tp_a_rendre.Data;

public static class DbInitializer
{
    public static void Seed(LocaticDbContext context)
    {
        if (context.Voitures.Any())
            return;

        var renault = new Marque { Nom = "Renault" };
        var peugeot = new Marque { Nom = "Peugeot" };
        context.Marques.AddRange(renault, peugeot);
        context.SaveChanges();

        var clio = new Modele { Nom = "Clio", MarqueId = renault.Id };
        var capture = new Modele { Nom = "Captur", MarqueId = renault.Id };
        var p208 = new Modele { Nom = "208", MarqueId = peugeot.Id };
        context.Modeles.AddRange(clio, capture, p208);
        context.SaveChanges();

        context.Voitures.AddRange(
            new Voiture { Immatriculation = "AB-123-CD", ModeleId = clio.Id },
            new Voiture { Immatriculation = "EF-456-GH", ModeleId = capture.Id },
            new Voiture { Immatriculation = "IJ-789-KL", ModeleId = p208.Id }
        );

        context.Clients.AddRange(
            new Client { Nom = "Martin", Prenom = "Sophie", Email = "sophie.martin@mail.fr", Telephone = "0612345678" },
            new Client { Nom = "Bernard", Prenom = "Lucas", Email = "lucas.bernard@mail.fr", Telephone = "0698765432" }
        );

        context.SaveChanges();
    }
}
