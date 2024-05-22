namespace MmorpgClassLibrary.Entiteti;

internal class OrudjeRestrictionRasa {
    protected internal virtual int Id { get; set; }
    protected internal virtual required string Rasa { get; set; }
    protected internal virtual required Oruzje Oruzje { get; set; }
}