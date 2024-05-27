using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class GrupaView {
    public int Id { get; set; } 
    
    public int? Dummy { get; set; }
    public IList<GroupMembershipView>? Clanovi { get; set; } = [];
    public IgraView? Igra { get; set; }

    internal GrupaView(Grupa? g) {
        if (g == null)
            return;
        Id = g.Id;
        Dummy = g.Dummy;
    }

    internal GrupaView(Grupa? g, Igra? i) : this(g) {
        Igra = new IgraView(i);
    }
}