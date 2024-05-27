namespace MmorpgClassLibrary.Entiteti;

internal class GroupMembership {
    protected internal virtual int Id { get; set; }
    protected internal virtual int? PobedjeniNeprijatelji { get; set; }
    protected internal virtual Igrac? Igrac { get; set; }
    protected internal virtual Grupa? Grupa { get; set; }
}