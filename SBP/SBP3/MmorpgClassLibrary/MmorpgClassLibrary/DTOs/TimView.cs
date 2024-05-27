using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class TimView {
    public int Id { get; set; }
    public string? Naziv { get; set; }
    public int? MinIgraca { get; set; }
    public int? MaxIgraca { get; set; }
    public int? BonusXp { get; set; }
    public IList<TeamMembershipView>? Clanovi { get; set; } = [];
    public IList<BoriSeView>? HomeBorbe { get; set; } = [];
    public IList<BoriSeView>? GuestBorbe { get; set; } = [];
    public IList<BoriSeView>? Pobede { get; set; } = [];
    
    public TimView() {}

    internal TimView(Tim? t) {
        if (t == null)
            return;
        Id = t.Id;
        Naziv = t.Naziv;
        MinIgraca = t.MinIgraca;
        MaxIgraca = t.MaxIgraca;
        BonusXp = t.BonusXp;
    }
}