using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Webshop.Domain.Entitites;
using Webshop.Domain.Enums;

namespace Webshop.Infrastructure.EF.Seeds;

public class SeederGenerator
{
    private readonly WebshopDbContext _ctx; // Kopplingen till databasen
    private readonly ILogger<SeederGenerator> _log;  // Loggar eventuella fel under seed-processen

    public SeederGenerator(WebshopDbContext ctx, ILogger<SeederGenerator> log) // Konstruktor
    {
        _ctx = ctx;
        _log = log;
    }

    private static string NormalizeEnumString(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return s;
        return s.Trim();
    }

    private static Färg InferColorFromName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return Färg.Okänd;
        var n = name.ToLowerInvariant();
        if (n.Contains("black") || n.Contains("svart")) return Färg.Svart;
        if (n.Contains("white") || n.Contains("vit")) return Färg.Vit;
        if (n.Contains("blue") || n.Contains("navy") || n.Contains("blå")) return Färg.Blå;
        if (n.Contains("red") || n.Contains("röd")) return Färg.Röd;
        if (n.Contains("green") || n.Contains("grön")) return Färg.Grön;
        if (n.Contains("pink") || n.Contains("rosa")) return Färg.Rosa;
        if (n.Contains("yellow") || n.Contains("gul")) return Färg.Gul;
        if (n.Contains("orange") || n.Contains("orange")) return Färg.Orange;
        if (n.Contains("purple") || n.Contains("lila")) return Färg.Lila;
        return Färg.Okänd;
    }

    private static Storlek InferSizeFromName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return Storlek.Okänd;
        var n = name.ToUpperInvariant();
        // För att undvika att "S" i "Small" eller "M" i "Medium"
        // felaktigt tolkas som storlekar, kollar vi både på förekomst av storlekstoken och kontexten runt den
        if (n.Contains("XXS")) return Storlek.XXS;
        if (n.Contains("XS")) return Storlek.XS;
        if (n.Contains("S") && (n.Contains(" SIZE ") || n.EndsWith(" S") || n.Contains(" S ") || n.Contains(" SMALL"))) return Storlek.S;
        if (n.Contains("M") && (n.Contains(" SIZE ") || n.EndsWith(" M") || n.Contains(" M ") || n.Contains(" MEDIUM"))) return Storlek.M;
        if (n.Contains("L") && (n.Contains(" SIZE ") || n.EndsWith(" L") || n.Contains(" L ") || n.Contains(" LARGE"))) return Storlek.L;
        if (n.Contains("XL") && !n.Contains("XXL")) return Storlek.XL;
        if (n.Contains("XXL")) return Storlek.XXL;

        // fallback: om storlek inte explicit nämns, kolla om namnet slutar på en storlekstoken
        if (n.EndsWith(" XS")) return Storlek.XS;
        if (n.EndsWith(" S")) return Storlek.S;
        if (n.EndsWith(" M")) return Storlek.M;
        if (n.EndsWith(" L")) return Storlek.L;
        if (n.EndsWith(" XL")) return Storlek.XL;
        if (n.EndsWith(" XXL")) return Storlek.XXL;
        return Storlek.Okänd;
    }

    public async Task SeedAsync(CancellationToken ct = default) // Huvudmetod som körs för att fylla databasen med initial data
    {
        await _ctx.Database.MigrateAsync(ct); // Säkerställer att databasen är uppdaterad med senaste schema

        using var tx = await _ctx.Database.BeginTransactionAsync(ct); // Startar en transaktion så att allt seedande sker atomärt
        try
        {
            // Find seed files. When running from different working directories the files might not be in the current directory.
            string ResolveSeedFile(string relativePath)
            {
                // try AppContext.BaseDirectory and walk up a few levels to find the repository root
                var start = AppContext.BaseDirectory;
                for (int i = 0; i < 6; i++)
                {
                    var candidate = Path.Combine(start, relativePath);
                    if (File.Exists(candidate)) return candidate;
                    start = Path.GetDirectoryName(start) ?? start;
                }
                // fallback to current directory search
                start = Directory.GetCurrentDirectory();
                for (int i = 0; i < 6; i++)
                {
                    var candidate = Path.Combine(start, relativePath);
                    if (File.Exists(candidate)) return candidate;
                    start = Path.GetDirectoryName(start) ?? start;
                }
                return Path.Combine(AppContext.BaseDirectory, relativePath); // last resort (may not exist)
            }

            var relative = Path.Combine("Webshop.Infrastructure", "EF", "Seeds", "Json");
            var masterPath = ResolveSeedFile(Path.Combine(relative, "Seeder.json"));
            var transPath = ResolveSeedFile(Path.Combine(relative, "SeederTransactions.json"));

            _log.LogInformation("DB: {conn}", _ctx.Database.GetDbConnection().ConnectionString);
            _log.LogInformation("Master JSON path: {p} exists={e}", masterPath, File.Exists(masterPath));
            _log.LogInformation("Trans JSON path: {p} exists={e}", transPath, File.Exists(transPath));

            if (File.Exists(masterPath)) // Kontrollerar att huvud-JSON-filen finns innan den läses
            {
                var masterJson = await File.ReadAllTextAsync(masterPath, ct); // Läser in JSON-innehållet som text
                using var doc = JsonDocument.Parse(masterJson); // Parser JSON-texten till ett JsonDocument för att kunna navigera i strukturen
                var root = doc.RootElement; // Huvudroten i JSON-strukturen

               
                if (root.TryGetProperty("Leverantorer", out var levers)) // Kontrollerar om "Leverantorer" finns i JSON och är en array
                {
                    foreach (var lev in levers.EnumerateArray()) // Loopar igenom varje element i "Leverantorer"-arrayen
                    {
                        var namn = lev.GetString(); // Hämtar namnet på leverantören som en sträng
                        if (string.IsNullOrWhiteSpace(namn)) continue; // Hoppar över tomma eller whitespace-namn
                        if (!await _ctx.Leverantörer.AnyAsync(l => l.Namn == namn, ct)) // Kontrollerar om en leverantör med samma namn redan finns i databasen
                        {
                            _ctx.Leverantörer.Add(new Leverantör { Id = Guid.NewGuid(), Namn = namn }); // Om inte, läggs en ny leverantör till i databasen med ett nytt GUID som Id
                        }
                    }
                }

                // FraktOmbud
                if (root.TryGetProperty("FraktOmbud", out var frakts)) // Kontrollerar om "FraktOmbud" finns i JSON och är en array
                {
                    foreach (var f in frakts.EnumerateArray()) // Loopar igenom varje element i "FraktOmbud"-arrayen
                    {
                        var namn = f.GetProperty("Namn").GetString(); // Hämtar namnet på fraktombudet som en sträng
                        var pris = f.GetProperty("Pris").GetDecimal(); // Hämtar priset på fraktombudet som en decimal
                        if (!await _ctx.FraktOmbud.AnyAsync(x => x.Namn == namn, ct)) // Kontrollerar om ett fraktombud med samma namn redan finns i databasen
                        {
                            _ctx.FraktOmbud.Add(new FraktOmbud { Id = Guid.NewGuid(), Namn = namn, Pris = pris }); // Om inte, läggs ett nytt fraktombud till i databasen med ett nytt GUID som Id
                        }
                    }
                }

                // Kategorier och Produkter
                if (root.TryGetProperty("Kategorier", out var cats)) // Kontrollerar om "Kategorier" finns i JSON och är en array
                {
                    foreach (var c in cats.EnumerateArray()) // Loopar igenom varje element i "Kategorier"-arrayen
                    {
                        var catName = c.GetProperty("Namn").GetString(); // Hämtar namnet på kategorin som en sträng
                        if (string.IsNullOrWhiteSpace(catName)) continue; // Hoppar över tomma eller whitespace-kategorinamn

                        var kategori = await _ctx.Kategorier.FirstOrDefaultAsync(k => k.Namn == catName, ct) ?? new Kategori { Id = Guid.NewGuid(), Namn = catName }; // Försöker hitta en befintlig kategori med samma namn i databasen, annars skapas en ny kategori med ett nytt GUID som Id
                        if (kategori.Id == Guid.Empty) kategori.Id = Guid.NewGuid(); // Om kategorin inte har ett Id, tilldelas ett nytt GUID
                        if (kategori.Produkter == null) kategori.Produkter = new List<Produkt>(); // Säkerställer att kategorin har en lista för produkter, även om den är tom
                        if (kategori.Id != Guid.Empty && !_ctx.Kategorier.Local.Contains(kategori)) // Om kategorin har ett giltigt Id och inte redan är spårad i den lokala kontexten, läggs den till i databasen
                        {
                            if (!await _ctx.Kategorier.AnyAsync(k => k.Namn == catName, ct)) // Kontrollerar om en kategori med samma namn redan finns i databasen
                                _ctx.Kategorier.Add(kategori); // Om inte, läggs den nya kategorin till i databasen
                        }

                        if (c.TryGetProperty("Produkter", out var prods)) // Kontrollerar om "Produkter" finns i kategorin och är en array
                        {
                            // choose a default leverantör if none provided
                            var defaultSupplier = await _ctx.Leverantörer.FirstOrDefaultAsync(ct); // Hämtar den första leverantören i databasen som en standardleverantör för produkter som inte har en specifik leverantör angiven
                            if (defaultSupplier == null) // Om det inte finns några leverantörer i databasen, skapas en standardleverantör
                            {
                                defaultSupplier = new Leverantör { Id = Guid.NewGuid(), Namn = "Default Supplier" }; // Skapar en ny leverantör med namnet "Default Supplier" och ett nytt GUID som Id
                                _ctx.Leverantörer.Add(defaultSupplier); // Lägger till den nya leverantören i databasen
                                await _ctx.SaveChangesAsync(ct); // Sparar ändringarna i databasen för att säkerställa att standardleverantören finns innan den används för produkter
                            }

                            foreach (var p in prods.EnumerateArray()) // Loopar igenom varje element i "Produkter"-arrayen inom kategorin
                            {
                                var prodName = p.GetProperty("Namn").GetString(); // Hämtar produktens namn från JSON
                                if (string.IsNullOrWhiteSpace(prodName)) continue; // Hoppar över produkter som inte har ett giltigt namn
                                var besk = p.TryGetProperty("Besk", out var b) ? b.GetString() : null; // Hämtar produktens beskrivning från JSON, eller sätter den till null om den inte finns
                                var pris = p.TryGetProperty("Pris", out var pr) ? pr.GetDecimal() : 0m; // Hämtar produktens pris från JSON, eller sätter det till 0 om det inte finns
                                var lager = p.TryGetProperty("Lager", out var l) ? l.GetInt32() : 0; // Hämtar produktens lagerantal från JSON, eller sätter det till 0 om det inte finns
                                // Färg och storlek: läs från JSON om angivet, annars försök inferera från produktnamn
                                Färg färg = Färg.Okänd;
                                Storlek storlek = Storlek.Okänd;
                                if (p.TryGetProperty("Färg", out var färgProp) || p.TryGetProperty("Farg", out färgProp))
                                {
                                    var färgStr = färgProp.GetString();
                                    if (!string.IsNullOrWhiteSpace(färgStr) && Enum.TryParse<Färg>(NormalizeEnumString(färgStr), true, out var parsedFärg))
                                        färg = parsedFärg;
                                }
                                if (p.TryGetProperty("Storlek", out var storProp))
                                {
                                    var storStr = storProp.GetString();
                                    if (!string.IsNullOrWhiteSpace(storStr) && Enum.TryParse<Storlek>(NormalizeEnumString(storStr), true, out var parsedStor))
                                        storlek = parsedStor;
                                }
                                // infer if still unknown
                                if (färg == Färg.Okänd) färg = InferColorFromName(prodName);
                                if (storlek == Storlek.Okänd) storlek = InferSizeFromName(prodName);

                                var exists = await _ctx.Produkter.AnyAsync(x => x.Namn == prodName && x.Kategori != null && x.Kategori.Namn == catName, ct); // Kontrollerar om en produkt med samma namn och kategori redan finns i databasen för att undvika dubbletter
                                if (!exists) // Om produkten inte redan finns, skapas en ny produkt och läggs till i databasen
                                {
                                    var prod = new Produkt // Skapar en ny produkt med de hämtade värdena och kopplar den till rätt kategori och leverantör
                                    {
                                        Id = Guid.NewGuid(),
                                        Namn = prodName,
                                        Beskrivning = besk,
                                        Pris = pris,
                                        LagerAntal = lager,
                                        Färg = färg,
                                        Storlek = storlek,
                                        KategoriId = kategori.Id,
                                        LeverantörId = defaultSupplier.Id,
                                        ProduktOrdrar = new List<ProduktOrder>()
                                    };
                                    _ctx.Produkter.Add(prod); // Lägger till den nya produkten i databasen
                                }
                            }
                        }
                    }
                }
            }

            var saved = await _ctx.SaveChangesAsync(ct);
            _log.LogInformation("SaveChanges returned {count}", saved);

            // Transactions file (customers, orders)
            if (File.Exists(transPath)) // Kontrollerar att transaktions-JSON-filen finns innan den läses
            {
                var transJson = await File.ReadAllTextAsync(transPath, ct); // Läser innehållet i transaktions-JSON-filen
                using var tdoc = JsonDocument.Parse(transJson); // Parser JSON-texten till ett JsonDocument för att kunna navigera i strukturen
                var troot = tdoc.RootElement; // Huvudroten i transaktions-JSON-strukturen

                if (troot.TryGetProperty("Kunder", out var kunder)) // Kontrollerar om "Kunder" finns i JSON och är en array
                {
                    foreach (var k in kunder.EnumerateArray()) // Itererar över varje kund i JSON-arrayen
                    {
                        var epost = k.GetProperty("Epost").GetString(); // Hämtar e-postadressen för kunden
                        if (string.IsNullOrWhiteSpace(epost)) continue; // Hoppar över kunder som inte har en giltig e-postadress
                        if (!await _ctx.Kunder.AnyAsync(x => x.Epost == epost, ct)) // Kontrollerar om en kund med samma e-postadress redan finns i databasen för att undvika dubbletter
                        {
                            string GetStringSafe(JsonElement e)
                            {
                                return e.ValueKind switch
                                {
                                    JsonValueKind.String => e.GetString(),
                                    JsonValueKind.Number => e.GetRawText(),
                                    _ => e.ToString()
                                };
                            }

                            var kund = new Kund // Skapar en ny kund med de hämtade värdena
                            {
                                Id = Guid.NewGuid(),
                                Namn = k.GetProperty("Namn").GetString(),
                                Adress = k.GetProperty("Adress").GetString(),
                                Stad = k.GetProperty("Stad").GetString(),
                                Postnummer = k.TryGetProperty("Postnummer", out var pn) ? GetStringSafe(pn) : null,
                                MobilNummer = k.TryGetProperty("MobilNummer", out var mn) ? GetStringSafe(mn) : null,
                                Epost = epost
                            };
                            _ctx.Kunder.Add(kund); // Lägger till den nya kunden i databasen
                        }
                    }
                }

                await _ctx.SaveChangesAsync(ct); // Sparar ändringar i databasen

                if (troot.TryGetProperty("Ordrar", out var ordrar)) // Kontrollerar om "Ordrar" finns i JSON och är en array
                {
                    foreach (var o in ordrar.EnumerateArray()) // Itererar över varje order i JSON-arrayen
                    {
                        var kundEpost = o.GetProperty("KundEpost").GetString(); // Hämtar e-postadressen för kunden som är kopplad till ordern
                        var kund = await _ctx.Kunder.FirstOrDefaultAsync(x => x.Epost == kundEpost, ct); // Försöker hitta kunden i databasen baserat på e-postadressen
                        if (kund == null) continue; // Hoppar över ordern om kunden saknas

                        var orderDatum = o.TryGetProperty("OrderDatum", out var od) ? od.GetDateTime() : DateTime.UtcNow; // Hämtar orderdatumet eller använder aktuellt datum om det saknas
                        // För att undvika dubbletter, kontrollerar om det redan finns en order för samma kund och datum i databasen innan den läggs till
                        if (await _ctx.Ordrar.AnyAsync(x => x.KundId == kund.Id && x.OrderDatum == orderDatum, ct)) // Kontrollerar om en order med samma kund och datum redan finns i databasen för att undvika dubbletter
                            continue; // Om en sådan order redan finns, hoppar över att lägga till den nya ordern

                        var fraktNamn = o.TryGetProperty("FraktOmbudNamn", out var fn) ? fn.GetString() : null; // Hämtar namnet på fraktombudet från JSON, eller sätter det till null om det inte finns
                        var frakt = !string.IsNullOrWhiteSpace(fraktNamn) ? await _ctx.FraktOmbud.FirstOrDefaultAsync(f => f.Namn == fraktNamn, ct) : null; // Försöker hitta fraktombudet i databasen baserat på namnet, eller sätter det till null om namnet är tomt eller whitespace

                        var totalPris = o.TryGetProperty("TotalPris", out var tp) ? tp.GetDecimal() : 0m; // Hämtar totalpriset för ordern från JSON, eller sätter det till 0 om det saknas

                        var order = new Order // Skapar en ny order med de hämtade värdena och kopplar den till rätt kund och fraktombud
                        {
                            Id = Guid.NewGuid(),
                            KundId = kund.Id,
                            OrderDatum = orderDatum,
                            FraktOmbudId = frakt != null ? frakt.Id : Guid.Empty,
                            TotalPris = totalPris,
                            ProduktOrdrar = new List<ProduktOrder>()
                        };

                        if (o.TryGetProperty("ProduktRader", out var prader)) // Kontrollerar om "ProduktRader" finns i ordern och är en array
                        {
                            foreach (var r in prader.EnumerateArray()) // Itererar över varje produkt i "ProduktRader"-arrayen inom ordern
                            {
                                var prodNamn = r.GetProperty("ProduktNamn").GetString(); // Hämtar produktnamnet från JSON
                                var catNamn = r.GetProperty("KategoriNamn").GetString(); // Hämtar kategorinamnet från JSON för att kunna hitta rätt produkt i databasen baserat på både namn och kategori
                                var antal = r.TryGetProperty("Antal", out var a) ? a.GetInt32() : 1; // Hämtar antalet av produkten som beställts från JSON, eller sätter det till 1 om det saknas
                                var prisvid = r.TryGetProperty("PrisvidKöp", out var pk) ? pk.GetDecimal() : 0m; // Hämtar priset vid köp från JSON, eller sätter det till 0 om det saknas

                                var produkt = await _ctx.Produkter.Include(p => p.Kategori).FirstOrDefaultAsync(p => p.Namn == prodNamn && p.Kategori != null && p.Kategori.Namn == catNamn, ct); // Försöker hitta produkten i databasen baserat på både produktnamn och kategorinamn för att säkerställa att rätt produkt kopplas till ordern, eller sätter den till null om den inte hittas
                                if (produkt == null) continue; // Hoppar över produktordern om produkten saknas i databasen

                                var po = new ProduktOrder // Skapar en ny ProduktOrder som kopplar produkten till ordern med det angivna antalet och priset vid köp
                                {
                                    Id = Guid.NewGuid(),
                                    ProduktId = produkt.Id,
                                    Produkt = produkt,
                                    Antal = antal,
                                    PrisvidKöp = prisvid
                                };
                                order.ProduktOrdrar.Add(po); // Lägger till ProduktOrder-objektet i orderns lista över produktordrar, vilket skapar relationen mellan ordern och produkten
                            }
                        }

                        _ctx.Ordrar.Add(order); // Lägger till ordern i databasen
                    }
                }
            }

            await _ctx.SaveChangesAsync(ct); // Sparar ändringarna i databasen
            await tx.CommitAsync(ct); // Bekräftar transaktionen
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Seed failed"); // Loggar eventuella fel som uppstår under seed-processen
            await tx.RollbackAsync(ct); // Återställer transaktionen vid fel
            throw; // Rethrow för att låta felet bubbla upp efter att det har loggats och transaktionen har rullats tillbaka
        }
    }
}
