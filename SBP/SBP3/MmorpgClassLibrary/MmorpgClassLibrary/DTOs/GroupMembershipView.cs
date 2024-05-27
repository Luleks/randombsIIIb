using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class GroupMembershipView {
    public int Id { get; set; }
    public int? PobedjeniNeprijatelji { get; set; }
    public IgracView? Igrac { get; set; }
    public GrupaView? Grupa { get; set; }

    public GroupMembershipView() {
    }

    internal GroupMembershipView(GroupMembership? gm) {
        if (gm == null)
            return;
        Id = gm.Id;
        PobedjeniNeprijatelji = gm.PobedjeniNeprijatelji;
        Igrac = new IgracView(gm.Igrac);
        Grupa = new GrupaView(gm.Grupa);
    }
}