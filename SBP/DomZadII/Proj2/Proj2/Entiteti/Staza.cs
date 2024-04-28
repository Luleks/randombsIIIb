namespace Proj2.Entiteti;

public class Staza {
    public virtual int Id { get; protected set; }
    public virtual required string Naziv { get; set; }
    public virtual int BonusXp { get; set; }
    public virtual bool TimskaStaza { get; set; }
    public virtual bool RestrictedStaza { get; set; }
    public virtual IList<Igra> Igranja { get; set; } = [];
    public virtual IList<StazaRestrictionKlasa> OgranicenjaKlase { get; set; } = [];
    public virtual IList<StazaRestrictionRasa> OgranicenjaRase { get; set; } = [];
}