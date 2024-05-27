namespace MmorpgClassLibrary.Entiteti;

internal abstract class Orudje {
    protected internal virtual int Id { get; set; }
    protected internal virtual string? Naziv { get; set; }
    protected internal virtual string? Opis { get; set; }
    protected internal virtual IList<OrudjeRestrictionRasa>? OgranicenjaRase { get; set; }
    protected internal virtual IList<OrudjeRestrictionKlasa>? OgranicenjaKlase { get; set; }

    internal Orudje() {
        OgranicenjaRase = new List<OrudjeRestrictionRasa>();
        OgranicenjaKlase = new List<OrudjeRestrictionKlasa>();
    }
}