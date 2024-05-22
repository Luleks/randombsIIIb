using System.Collections.Generic;

namespace SBP2.Models.Entiteti;

public class Staza {
    public virtual int Id { get; protected set; }
    public virtual required string Naziv { get; set; }
    public virtual int BonusXp { get; set; }
    public virtual int TimskaStaza { get; set; } // boolean
    public virtual int RestrictedStaza { get; set; } // boolan
    public virtual IList<Igra> Igranja { get; set; } = []; 
    public virtual IList<StazaRestrictionKlasa> OgranicenjaKlase { get; set; } = [];
    public virtual IList<StazaRestrictionRasa> OgranicenjaRase { get; set; } = [];
}