namespace MmorpgClassLibrary.Entiteti;

internal class Sesija {
    protected internal virtual int Id { get; set; }
    protected internal virtual int Zlato { get; set; }
    protected internal virtual int Xp { get; set; }
    protected internal virtual DateTime Vreme { get; set; }
    protected internal virtual int Duzina { get; set; }
    protected internal virtual required Igrac Igrac { get; set; }
}