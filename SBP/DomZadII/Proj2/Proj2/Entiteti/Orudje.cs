namespace Proj2.Entiteti;

public abstract class Orudje {
    public virtual int Id { get; protected set; }
    public virtual required string Naziv { get; set; }
    public virtual required string Opis { get; set; }
    public virtual IList<OrudjeRestrictionRasa> OgranicenjaRase { get; set; } = [];
    public virtual IList<OrudjeRestrictionKlasa> OgranicenjaKlase { get; set; } = [];
}