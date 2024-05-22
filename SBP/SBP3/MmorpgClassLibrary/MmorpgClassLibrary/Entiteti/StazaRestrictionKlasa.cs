namespace MmorpgClassLibrary.Entiteti;

internal class StazaRestrictionKlasa {
    protected internal virtual int Id { get; set; }
    protected internal virtual required string Klasa { get; set; }
    protected internal virtual required Staza Staza { get; set; }
}