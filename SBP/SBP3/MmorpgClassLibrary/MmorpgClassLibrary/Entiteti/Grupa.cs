namespace MmorpgClassLibrary.Entiteti;

internal class Grupa {
    protected internal virtual int Id { get; set; } 
    protected internal virtual int? Dummy { get; set; }
    protected internal virtual IList<GroupMembership>? Clanovi { get; set; } 
    protected internal virtual Igra? Igra { get; set; }

    internal Grupa() {
        Clanovi = new List<GroupMembership>();
    }
}