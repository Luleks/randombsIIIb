namespace MmorpgClassLibrary.Entiteti;

internal class Igrac {
    protected internal virtual int Id { get; set; }
    protected internal virtual string? Nadimak { get; set; }
    protected internal virtual string? Lozinka { get; set; }
    protected internal virtual char? Pol { get; set; }
    protected internal virtual string? Ime { get; set; }
    protected internal virtual string? Prezime { get; set; }
    protected internal virtual int? Uzrast { get; set; }
    protected internal virtual Lik? Lik { get; set; }
    protected internal virtual IList<Sesija>? Sesije { get; set; }
    protected internal virtual IList<Pomocnik>? Pomocnici { get; set; }
    protected internal virtual IList<TeamMembership>? Timovi { get; set; }
    protected internal virtual IList<JeKupio>? Kupovine { get; set; }
    protected internal virtual IList<Poseduje>? KljucniPredmeti { get; set; }
    protected internal virtual IList<GroupMembership>? Grupe { get; set; }

    internal Igrac() {
        Sesije = new List<Sesija>();
        Pomocnici = new List<Pomocnik>();
        Timovi = new List<TeamMembership>();
        Kupovine = new List<JeKupio>();
        KljucniPredmeti = new List<Poseduje>();
        Grupe = new List<GroupMembership>();
    }
    
}