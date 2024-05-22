namespace MmorpgClassLibrary.Entiteti;

internal class StazaRestrictionRasa {
    protected internal virtual int Id { get; set; }
    protected internal virtual required string Rasa { get; set; }
    protected internal virtual required Staza Staza { get; set; }
}