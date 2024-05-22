namespace MmorpgClassLibrary.Entiteti;

internal class Pomocnik {
    protected internal virtual int Id { get; set; }
    protected internal virtual required string Ime { get; set; }
    protected internal virtual required string Rasa { get; set; }
    protected internal virtual required string Klasa { get; set; }
    protected internal virtual int BonusZastita { get; set; }
    protected internal virtual required Igrac Igrac { get; set; }
    
}