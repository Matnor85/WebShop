using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using System.Linq;


namespace Webshop.Infrastructure.EF.Seeds
{
    public static class WebShopSeeder
    {
        public static void Seed(WebshopDbContext context)
        {
            var klader = GetOrCreateKategori(context, "Kläder");
            var skor = GetOrCreateKategori(context, "Skor");
            var accessoarer = GetOrCreateKategori(context, "Accessoarer");

            var nike = GetOrCreateLeverantor(context, "Nike");
            var adidas = GetOrCreateLeverantor(context, "Adidas");
            var puma = GetOrCreateLeverantor(context, "Puma");

            var standardFrakt = GetOrCreateFrakt(context, "Standard Frakt", 49.99m);
            var expressFrakt = GetOrCreateFrakt(context, "Express Frakt", 99.99m);

            AddProduktIfMissing(context, "Vit T-shirt", "En vit T-shirt i bomull", 199.99m, "Vit", "M", 40, klader.Id, nike.Id);
            AddProduktIfMissing(context, "Svarta Sneakers", "Svarta sneakers med bra grepp", 899.99m, "Svart", "XL", 25, skor.Id, adidas.Id);
            AddProduktIfMissing(context, "Röd Keps", "En röd keps med justerbar storlek", 149.99m, "Röd", "One Size", 60, accessoarer.Id, puma.Id);
            AddProduktIfMissing(context, "Jeans", "Blå jeans med stretch", 499.99m, "Blå", "L", 30, klader.Id, nike.Id);
            AddProduktIfMissing(context, "Sandaler", "Gröna sandaler för sommaren", 299.99m, "Grön", "38", 20, skor.Id, puma.Id);
            AddProduktIfMissing(context, "Svart Bälte", "Ett svart bälte i läder", 249.99m, "Svart", "One Size", 50, accessoarer.Id, adidas.Id);

            var anna = GetOrCreateKund(context, "anna.andersson@example.com", "Anna Andersson", "Storgatan 1", "Stockholm", 41105, 0701234567);
            var lars = GetOrCreateKund(context, "lars.larsson@example.com", "Lars Larsson", "Lilla Vägen 2", "Göteborg", 41106, 0702345678);
            var eva = GetOrCreateKund(context, "eva.eriksson@example.com", "Eva Eriksson", "Södra Gatan 3", "Malmö", 41107, 0703456789);
            GetOrCreateKund(context, "per.persson@example.com", "Per Persson", "Norra Vägen 4", "Uppsala", 41108, 0704567890);

            if (!context.Ordrar.Any())
            {
                context.Ordrar.AddRange(
                    new Order { Id = Guid.NewGuid(), OrderDatum = DateTime.Now.AddDays(-4), KundId = anna.Id, FraktOmbudId = standardFrakt.Id, TotalPris = 1198.99m },
                    new Order { Id = Guid.NewGuid(), OrderDatum = DateTime.Now.AddDays(-2), KundId = lars.Id, FraktOmbudId = expressFrakt.Id, TotalPris = 899.99m },
                    new Order { Id = Guid.NewGuid(), OrderDatum = DateTime.Now.AddDays(-1), KundId = eva.Id, FraktOmbudId = standardFrakt.Id, TotalPris = 1876.99m }
                );
            }

            context.SaveChanges();
        }

        private static Kategori GetOrCreateKategori(WebshopDbContext context, string namn)
        {
            var existing = context.Kategorier.FirstOrDefault(k => k.Namn == namn);
            if (existing is not null) return existing;

            var kategori = new Kategori { Id = Guid.NewGuid(), Namn = namn };
            context.Kategorier.Add(kategori);
            return kategori;
        }

        private static Leverantör GetOrCreateLeverantor(WebshopDbContext context, string namn)
        {
            var existing = context.Leverantörer.FirstOrDefault(l => l.Namn == namn);
            if (existing is not null) return existing;

            var leverantor = new Leverantör { Id = Guid.NewGuid(), Namn = namn };
            context.Leverantörer.Add(leverantor);
            return leverantor;
        }

        private static FraktOmbud GetOrCreateFrakt(WebshopDbContext context, string namn, decimal pris)
        {
            var existing = context.Set<FraktOmbud>().FirstOrDefault(f => f.Namn == namn);
            if (existing is not null) return existing;

            var frakt = new FraktOmbud { Id = Guid.NewGuid(), Namn = namn, Pris = pris };
            context.Set<FraktOmbud>().Add(frakt);
            return frakt;
        }

        private static void AddProduktIfMissing(
            WebshopDbContext context,
            string namn,
            string beskrivning,
            decimal pris,
            string farg,
            string storlek,
            int lagerAntal,
            Guid kategoriId,
            Guid leverantorId)
        {
            if (context.Produkter.Any(p => p.Namn == namn)) return;

            context.Produkter.Add(new Produkt
            {
                Id = Guid.NewGuid(),
                Namn = namn,
                Beskrivning = beskrivning,
                Pris = pris,
                Färg = farg,
                Storlek = storlek,
                LagerAntal = lagerAntal,
                KategoriId = kategoriId,
                LeverantörId = leverantorId
            });
        }

        private static Kund GetOrCreateKund(
            WebshopDbContext context,
            string epost,
            string namn,
            string adress,
            string stad,
            int postnummer,
            int mobilNummer)
        {
            var existing = context.Kunder.FirstOrDefault(k => k.Epost == epost);
            if (existing is not null) return existing;

            var kund = new Kund
            {
                Id = Guid.NewGuid(),
                Epost = epost,
                Namn = namn,
                Adress = adress,
                Stad = stad,
                Postnummer = postnummer,
                MobilNummer = mobilNummer
            };
            context.Kunder.Add(kund);
            return kund;
        }
    }


}
