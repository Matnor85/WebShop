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
        // Säkerställer att databasen är uppdaterad med senaste schema
        await _ctx.Database.MigrateAsync(ct);

        // Startar en transaktion så att allt seedande sker atomärt
        using var tx = await _ctx.Database.BeginTransactionAsync(ct);
        try
        {
            /* För att göra det mer robust att hitta seed-filerna, oavsett var applikationen körs ifrån, försöker vi först hitta dem relativt
               till AppContext.BaseDirectory och går uppåt i katalogstrukturen. Om det inte lyckas, försöker vi från CurrentDirectory.
               Detta gör att det fungerar både i utvecklingsmiljöer och när applikationen är publicerad. */
            string ResolveSeedFile(string relativePath)
            {
                // Försök hitta filen genom att gå uppåt i katalogstrukturen från AppContext.BaseDirectory
                var start = AppContext.BaseDirectory;
                for (int i = 0; i < 6; i++)
                {
                    var candidate = Path.Combine(start, relativePath);
                    if (File.Exists(candidate)) return candidate;
                    start = Path.GetDirectoryName(start) ?? start;
                }
                // Om det inte hittas där, försök från CurrentDirectory
                start = Directory.GetCurrentDirectory();
                for (int i = 0; i < 6; i++)
                {
                    var candidate = Path.Combine(start, relativePath);
                    if (File.Exists(candidate)) return candidate;
                    start = Path.GetDirectoryName(start) ?? start;
                }
                // Om det inte hittas alls, returnera den ursprungliga sökvägen relativt till AppContext.BaseDirectory som en sista utväg
                // (vilket sannolikt inte kommer att fungera)
                return Path.Combine(AppContext.BaseDirectory, relativePath);
            }

            var relative = Path.Combine("Webshop.Infrastructure", "EF", "Seeds", "Json");
            var masterPath = ResolveSeedFile(Path.Combine(relative, "Seeder.json"));
            var transPath = ResolveSeedFile(Path.Combine(relative, "SeederTransactions.json"));

            _log.LogInformation("DB: {conn}", _ctx.Database.GetDbConnection().ConnectionString);
            _log.LogInformation("Master JSON path: {p} exists={e}", masterPath, File.Exists(masterPath));
            _log.LogInformation("Trans JSON path: {p} exists={e}", transPath, File.Exists(transPath));

            // Kontrollerar att huvud-JSON-filen finns innan den läses
            if (File.Exists(masterPath))
            {
                // Läser in JSON-innehållet som text
                var masterJson = await File.ReadAllTextAsync(masterPath, ct);
                // Parser JSON-texten till ett JsonDocument för att kunna navigera i strukturen
                using var doc = JsonDocument.Parse(masterJson);
                // Huvudroten i JSON-strukturen
                var root = doc.RootElement;

                // Kontrollerar om "Leverantorer" finns i JSON och är en array
                if (root.TryGetProperty("Leverantorer", out var levers))
                {
                    // Loopar igenom varje element i "Leverantorer"-arrayen
                    foreach (var lev in levers.EnumerateArray())
                    {
                        // Hämtar namnet på leverantören som en sträng
                        var namn = lev.GetString();
                        // Hoppar över tomma eller whitespace-namn
                        if (string.IsNullOrWhiteSpace(namn)) continue;
                        // Kontrollerar om en leverantör med samma namn redan finns i databasen
                        if (!await _ctx.Leverantörer.AnyAsync(l => l.Namn == namn, ct))
                        {
                            // Om inte, läggs en ny leverantör till i databasen med ett nytt GUID som Id
                            _ctx.Leverantörer.Add(new Leverantör { Id = Guid.NewGuid(), Namn = namn });
                        }
                    }
                }

                // FraktOmbud

                // Kontrollerar om "FraktOmbud" finns i JSON och är en array
                if (root.TryGetProperty("FraktOmbud", out var frakts))
                {
                    // Loopar igenom varje element i "FraktOmbud"-arrayen
                    foreach (var f in frakts.EnumerateArray())
                    {
                        // Hämtar namnet på fraktombudet som en sträng
                        var namn = f.GetProperty("Namn").GetString();
                        // Hämtar priset på fraktombudet som en decimal
                        var pris = f.GetProperty("Pris").GetDecimal();
                        // Kontrollerar om ett fraktombud med samma namn redan finns i databasen
                        if (!await _ctx.FraktOmbud.AnyAsync(x => x.Namn == namn, ct))
                        {
                            // Om inte, läggs ett nytt fraktombud till i databasen med ett nytt GUID som Id
                            _ctx.FraktOmbud.Add(new FraktOmbud { Id = Guid.NewGuid(), Namn = namn, Pris = pris });
                        }
                    }
                }

                // Kategorier och Produkter

                // Kontrollerar om "Kategorier" finns i JSON och är en array
                if (root.TryGetProperty("Kategorier", out var cats))
                {
                    // Loopar igenom varje element i "Kategorier"-arrayen
                    foreach (var c in cats.EnumerateArray())
                    {
                        // Hämtar namnet på kategorin som en sträng
                        var catName = c.GetProperty("Namn").GetString();
                        // Hoppar över tomma eller whitespace-kategorinamn
                        if (string.IsNullOrWhiteSpace(catName)) continue;

                        // Försöker hitta en befintlig kategori med samma namn i databasen, annars skapas en ny kategori med ett nytt GUID som Id
                        var kategori = await _ctx.Kategorier
                            .FirstOrDefaultAsync(k => k.Namn == catName, ct) ?? new Kategori { Id = Guid.NewGuid(), Namn = catName };

                        // Om kategorin inte har ett Id, tilldelas ett nytt GUID
                        if (kategori.Id == Guid.Empty) kategori.Id = Guid.NewGuid();

                        // Säkerställer att kategorin har en lista för produkter, även om den är tom
                        if (kategori.Produkter == null) kategori.Produkter = new List<Produkt>();

                        // Om kategorin har ett giltigt Id och inte redan är spårad i den lokala kontexten, läggs den till i databasen
                        if (kategori.Id != Guid.Empty && !_ctx.Kategorier.Local.Contains(kategori))
                        {
                            // Kontrollerar om en kategori med samma namn redan finns i databasen
                            if (!await _ctx.Kategorier.AnyAsync(k => k.Namn == catName, ct))
                                // Om inte, läggs den nya kategorin till i databasen
                                _ctx.Kategorier.Add(kategori);
                        }

                        // Kontrollerar om "Produkter" finns i kategorin och är en array
                        if (c.TryGetProperty("Produkter", out var prods))
                        {

                            // Hämtar den första leverantören i databasen som en standardleverantör för produkter som inte har en
                            // specifik leverantör angiven
                            var defaultSupplier = await _ctx.Leverantörer.FirstOrDefaultAsync(ct);

                            // Om det inte finns några leverantörer i databasen, skapas en standardleverantör
                            if (defaultSupplier == null)
                            {
                                // Skapar en ny leverantör med namnet "Default Supplier" och ett nytt GUID som Id
                                defaultSupplier = new Leverantör { Id = Guid.NewGuid(), Namn = "Default Supplier" };
                                // Lägger till den nya leverantören i databasen
                                _ctx.Leverantörer.Add(defaultSupplier);
                                await _ctx.SaveChangesAsync(ct);
                            }

                            // Loopar igenom varje element i "Produkter"-arrayen inom kategorin
                            foreach (var p in prods.EnumerateArray())
                            {
                                // Hämtar produktens namn från JSON
                                var prodName = p.GetProperty("Namn").GetString();

                                // Hoppar över produkter som inte har ett giltigt namn
                                if (string.IsNullOrWhiteSpace(prodName)) continue;

                                // Hämtar produktens beskrivning från JSON, eller sätter den till null om den inte finns
                                var besk = p.TryGetProperty("Besk", out var b) ? b.GetString() : null;

                                // Hämtar produktens pris från JSON, eller sätter det till 0 om det inte finns
                                var pris = p.TryGetProperty("Pris", out var pr) ? pr.GetDecimal() : 0m;

                                // Hämtar produktens lagerantal från JSON, eller sätter det till 0 om det inte finns
                                var lager = p.TryGetProperty("Lager", out var l) ? l.GetInt32() : 0;

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
                                // Om färg eller storlek inte explicit anges i JSON, försök inferera dem från produktnamnet
                                if (färg == Färg.Okänd) färg = InferColorFromName(prodName);
                                if (storlek == Storlek.Okänd) storlek = InferSizeFromName(prodName);

                                // Kontrollerar om en produkt med samma namn och kategori redan finns i databasen för att undvika dubbletter
                                var exists = await _ctx.Produkter.AnyAsync(x => x.Namn == prodName && x.Kategori != null && x.Kategori.Namn == catName, ct);

                                // Om produkten inte redan finns, skapas en ny produkt och läggs till i databasen
                                if (!exists)
                                {
                                    var prod = new Produkt
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
                                    _ctx.Produkter.Add(prod);
                                }
                            }
                        }
                    }
                }
            }

            var saved = await _ctx.SaveChangesAsync(ct);
            _log.LogInformation("SaveChanges returned {count}", saved);

            // Transactions file (customers, orders)
            // Kontrollerar att transaktions-JSON-filen finns innan den läses
            if (File.Exists(transPath))
            {
                // Läser innehållet i transaktions-JSON-filen
                var transJson = await File.ReadAllTextAsync(transPath, ct);
                // Parser JSON-texten till ett JsonDocument för att kunna navigera i strukturen
                using var tdoc = JsonDocument.Parse(transJson);
                // Huvudroten i transaktions-JSON-strukturen
                var troot = tdoc.RootElement;

                // Kontrollerar om "Kunder" finns i JSON och är en array
                if (troot.TryGetProperty("Kunder", out var kunder))
                {
                    // Itererar över varje kund i JSON-arrayen
                    foreach (var k in kunder.EnumerateArray())
                    {
                        // Hämtar e-postadressen för kunden
                        var epost = k.GetProperty("Epost").GetString();
                        // Hoppar över kunder som inte har en giltig e-postadress
                        if (string.IsNullOrWhiteSpace(epost)) continue;
                        // Kontrollerar om en kund med samma e-postadress redan finns i databasen för att undvika dubbletter
                        if (!await _ctx.Kunder.AnyAsync(x => x.Epost == epost, ct))
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

                            // Skapar en ny kund med de hämtade värdena
                            var kund = new Kund
                            {
                                Id = Guid.NewGuid(),
                                Namn = k.GetProperty("Namn").GetString(),
                                Adress = k.GetProperty("Adress").GetString(),
                                Stad = k.GetProperty("Stad").GetString(),
                                Postnummer = k.TryGetProperty("Postnummer", out var pn) ? GetStringSafe(pn) : null,
                                MobilNummer = k.TryGetProperty("MobilNummer", out var mn) ? GetStringSafe(mn) : null,
                                Epost = epost
                            };
                            _ctx.Kunder.Add(kund);
                        }
                    }
                }

                // Sparar ändringar i databasen
                await _ctx.SaveChangesAsync(ct);

                // Kontrollerar om "Ordrar" finns i JSON och är en array
                if (troot.TryGetProperty("Ordrar", out var ordrar))
                {
                    // Itererar över varje order i JSON-arrayen
                    foreach (var o in ordrar.EnumerateArray())
                    {
                        // Hämtar e-postadressen för kunden som är kopplad till ordern
                        var kundEpost = o.GetProperty("KundEpost").GetString();
                        // Försöker hitta kunden i databasen baserat på e-postadressen
                        var kund = await _ctx.Kunder.FirstOrDefaultAsync(x => x.Epost == kundEpost, ct);
                        // Hoppar över ordern om kunden saknas
                        if (kund == null) continue;

                        // Hämtar orderdatumet eller använder aktuellt datum om det saknas
                        var orderDatum = o.TryGetProperty("OrderDatum", out var od) ? od.GetDateTime() : DateTime.UtcNow;
                        // Kontrollerar om en order med samma kund och datum redan finns i databasen för att undvika dubbletter
                        if (await _ctx.Ordrar.AnyAsync(x => x.KundId == kund.Id && x.OrderDatum == orderDatum, ct))
                            // Om en sådan order redan finns, hoppar över att lägga till den nya ordern
                            continue;

                        // Hämtar namnet på fraktombudet från JSON, eller sätter det till null om det inte finns
                        var fraktNamn = o.TryGetProperty("FraktOmbudNamn", out var fn) ? fn.GetString() : null;
                        // Försöker hitta fraktombudet i databasen baserat på namnet, eller sätter det till null om namnet är tomt eller whitespace
                        var frakt = !string.IsNullOrWhiteSpace(fraktNamn) ? await _ctx.FraktOmbud.FirstOrDefaultAsync(f => f.Namn == fraktNamn, ct) : null;

                        // Hämtar totalpriset för ordern från JSON, eller sätter det till 0 om det saknas
                        var totalPris = o.TryGetProperty("TotalPris", out var tp) ? tp.GetDecimal() : 0m;

                        // Skapar en ny order med de hämtade värdena och kopplar den till rätt kund och fraktombud
                        var order = new Order
                        {
                            Id = Guid.NewGuid(),
                            KundId = kund.Id,
                            OrderDatum = orderDatum,
                            FraktOmbudId = frakt != null ? frakt.Id : Guid.Empty,
                            TotalPris = totalPris,
                            ProduktOrdrar = new List<ProduktOrder>()
                        };

                        // Kontrollerar om "ProduktRader" finns i ordern och är en array
                        if (o.TryGetProperty("ProduktRader", out var prader))
                        {
                            // Itererar över varje produkt i "ProduktRader"-arrayen inom ordern
                            foreach (var r in prader.EnumerateArray())
                            {
                                // Hämtar produktnamnet från JSON
                                var prodNamn = r.GetProperty("ProduktNamn").GetString(); 
                                // Hämtar kategorinamnet från JSON för att kunna hitta rätt produkt i databasen baserat på både namn och kategori
                                var catNamn = r.GetProperty("KategoriNamn").GetString(); 
                                // Hämtar antalet av produkten som beställts från JSON, eller sätter det till 1 om det saknas
                                var antal = r.TryGetProperty("Antal", out var a) ? a.GetInt32() : 1; 
                                // Hämtar priset vid köp från JSON, eller sätter det till 0 om det saknas
                                var prisvid = r.TryGetProperty("PrisvidKöp", out var pk) ? pk.GetDecimal() : 0m; 

                                var produkt = await _ctx.Produkter.Include(p => p.Kategori).FirstOrDefaultAsync(p => p.Namn == prodNamn && p.Kategori != null && p.Kategori.Namn == catNamn, ct); // Försöker hitta produkten i databasen baserat på både produktnamn och kategorinamn för att säkerställa att rätt produkt kopplas till ordern, eller sätter den till null om den inte hittas
                                // Hoppar över produktordern om produkten saknas i databasen
                                if (produkt == null) continue; 

                                // Skapar en ny ProduktOrder som kopplar produkten till ordern med det angivna antalet och priset vid köp
                                var po = new ProduktOrder 
                                {
                                    Id = Guid.NewGuid(),
                                    ProduktId = produkt.Id,
                                    Produkt = produkt,
                                    Antal = antal,
                                    PrisvidKöp = prisvid
                                };
                                order.ProduktOrdrar.Add(po); 
                            }
                        }

                        _ctx.Ordrar.Add(order); 
                    }
                }
            }
            // Sparar alla ändringar i databasen inom transaktionen
            await _ctx.SaveChangesAsync(ct); 
            // Bekräftar transaktionen
            await tx.CommitAsync(ct); 
        }
        catch (Exception ex)
        {
            // Loggar eventuella fel som uppstår under seed-processen
            _log.LogError(ex, "Seed failed"); 
            // Återställer transaktionen vid fel
            await tx.RollbackAsync(ct); 
            // Rethrow för att låta felet bubbla upp efter att det har loggats och transaktionen har rullats tillbaka
            throw; 
        }
    }
}