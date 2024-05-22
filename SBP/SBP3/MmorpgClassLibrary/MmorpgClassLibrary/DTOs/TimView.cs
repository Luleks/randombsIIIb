namespace MmorpgClassLibrary.DTOs;

public class TimView {
    public int Id { get; protected set; }
    public string? Naziv { get; set; }
    public int? MinIgraca { get; set; }
    public int? MaxIgraca { get; set; }
    public int? BonusXp { get; set; }
    public IList<TeamMembershipView>? Clanovi { get; set; }
    public IList<BoriSeView>? HomeBorbe { get; set; }
    public IList<BoriSeView>? GuestBorbe { get; set; }
    public IList<BoriSeView>? Pobede { get; set; }
}