namespace MmorpgClassLibrary.Entiteti;

internal abstract class Orudje {
    protected internal virtual int Id { get; set; }
    protected internal virtual required string Naziv { get; set; }
    protected internal virtual required string Opis { get; set; }
    protected internal virtual IList<OrudjeRestrictionRasa> OgranicenjaRase { get; set; } = [];
    protected internal virtual IList<OrudjeRestrictionKlasa> OgranicenjaKlase { get; set; } = [];
}