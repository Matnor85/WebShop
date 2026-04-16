namespace Webshop.Domain.Entitites;

public class Kund
{
    public Guid Id { get; set; }
    public string Namn { get; set; }
    public string Adress { get; set; }
    public string Stad { get; set; }
    public string Postnummer { get; set; }
    public string MobilNummer { get; set; }
    public string Epost { get; set; }
    public List<Order> Ordrar { get; set; } = new List<Order>();

    public Kund() { }

    public Kund(string namn, string adress, string stad, string postnummer, string mobilNummer, string epost)
    {
        Id = Guid.NewGuid();
        Namn = namn;
        Adress = adress;
        Stad = stad;
        Postnummer = postnummer;
        MobilNummer = mobilNummer;
        Epost = epost;
    }
    public override string ToString()
    {
        return $"Namn: {Namn}\nAdress: {Adress}\nStad: {Stad}\nPostnummer: {Postnummer}\nTelefonnummer: {MobilNummer}\nE-post: {Epost}";
    }
}