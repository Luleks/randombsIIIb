namespace MmorpgClassLibrary.Entiteti;

internal class GroupMembership {
    protected internal virtual int Id { get; set; }
    protected internal virtual int PobedjeniNeprijatelji { get; set; }
    protected internal virtual required Igrac Igrac { get; set; }
    protected internal virtual required Grupa Grupa { get; set; }
}