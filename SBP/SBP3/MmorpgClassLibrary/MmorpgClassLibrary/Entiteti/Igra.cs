namespace MmorpgClassLibrary.Entiteti;

internal class Igra {
    protected internal virtual int Id { get; set; }
    protected internal virtual DateTime? Vreme { get; set; }
    protected internal virtual Grupa? Grupa { get; set; }
    protected internal virtual Staza? Staza { get; set; }
    protected internal virtual Orudje? FindableOrudje { get; set; }
}