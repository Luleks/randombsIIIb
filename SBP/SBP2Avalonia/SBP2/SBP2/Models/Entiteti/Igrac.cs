using System.Collections.Generic;

namespace SBP2.Models.Entiteti;

public class Igrac {
    public virtual int Id { get; protected set; }
    public virtual required string Nadimak { get; set; }
    public virtual required string Lozinka { get; set; }
    public virtual char Pol { get; set; }
    public virtual required string Ime { get; set; }
    public virtual string? Prezime { get; set; }
    public virtual int Uzrast { get; set; }
    public virtual required Lik Lik { get; set; }
    public virtual IList<Sesija> Sesije { get; set; } = [];
    public virtual IList<Pomocnik> Pomocnici { get; set; } = [];
    public virtual IList<TeamMembership> Timovi { get; set; } = [];
    public virtual IList<JeKupio> Kupovine { get; set; } = [];
    public virtual IList<Poseduje> KljucniPredmeti { get; set; } = [];
    public virtual IList<GroupMembership> Grupe { get; set; } = [];
}