namespace MmorpgClassLibrary.Entiteti;

internal class Staza {
    protected internal virtual int Id { get; set; }
    protected internal virtual required string Naziv { get; set; }
    protected internal virtual int BonusXp { get; set; }
    protected internal virtual int TimskaStaza { get; set; } // boolean
    protected internal virtual int RestrictedStaza { get; set; } // boolan
    protected internal virtual IList<Igra> Igranja { get; set; } = []; 
    protected internal virtual IList<StazaRestrictionKlasa> OgranicenjaKlase { get; set; } = [];
    protected internal virtual IList<StazaRestrictionRasa> OgranicenjaRase { get; set; } = [];
}