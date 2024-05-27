namespace MmorpgClassLibrary.Entiteti;

internal class TeamMembership {
    protected internal virtual int Id { get; set; }
    protected internal virtual DateTime? VremeOd { get; set; }
    protected internal virtual DateTime? VremeDo { get; set; }
    protected internal virtual Igrac? Igrac { get; set; }
    protected internal virtual Tim? Tim { get; set; }
}