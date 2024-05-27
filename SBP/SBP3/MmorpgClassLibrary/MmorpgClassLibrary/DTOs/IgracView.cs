using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class IgracView {
    public int Id { get; set; }
    public string? Nadimak { get; set; }
    public string? Lozinka { get; set; }
    public char? Pol { get; set; }
    public string? Ime { get; set; }
    public string? Prezime { get; set; }
    public int? Uzrast { get; set; }
    public LikView? Lik { get; set; }
    public IList<SesijaView>? Sesije { get; set; } = [];
    public IList<PomocnikView>? Pomocnici { get; set; } = [];
    public IList<TeamMembershipView>? Timovi { get; set; } = [];
    public IList<JeKupioView>? Kupovine { get; set; } = [];
    public IList<PosedujeView>? KljucniPredmeti { get; set; } = [];
    public IList<GroupMembershipView>? Grupe { get; set; } = [];

    public IgracView() {
    }

    internal IgracView(Igrac? i) {
        if (i == null)
            return;
        Id = i.Id;
        Nadimak = i.Nadimak;
        Lozinka = i.Lozinka;
        Pol = i.Pol;
        Ime = i.Ime;
        Prezime = i.Prezime;
        Uzrast = i.Uzrast;
        Lik = new LikView(i.Lik);
    }
}