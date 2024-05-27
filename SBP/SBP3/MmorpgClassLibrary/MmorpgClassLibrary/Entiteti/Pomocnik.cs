namespace MmorpgClassLibrary.Entiteti;

internal class Pomocnik {
    protected internal virtual int Id { get; set; }
    protected internal virtual string? Ime { get; set; }
    protected internal virtual string? Rasa { get; set; }
    protected internal virtual string? Klasa { get; set; }
    protected internal virtual int? BonusZastita { get; set; }
    protected internal virtual Igrac? Igrac { get; set; }
    
}