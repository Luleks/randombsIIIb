namespace MmorpgClassLibrary.Entiteti;

internal class OrudjeRestrictionKlasa {
    protected internal virtual int Id { get; set; }
    protected internal virtual required string Klasa { get; set; }
    protected internal virtual required Oruzje Oruzje { get; set; }
}