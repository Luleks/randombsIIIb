using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class TeamMembershipView {
    public int Id { get; set; }
    public DateTime? VremeOd { get; set; }
    public DateTime? VremeDo { get; set; }
    public IgracView? Igrac { get; set; }
    public TimView? Tim { get; set; }

    public TeamMembershipView() {
    }

    internal TeamMembershipView(TeamMembership? t) {
        if (t == null)
            return;
        Id = t.Id;
        VremeOd = t.VremeOd;
        VremeDo = t.VremeDo;
        Igrac = new IgracView(t.Igrac);
        Tim = new TimView(t.Tim);
    }
}