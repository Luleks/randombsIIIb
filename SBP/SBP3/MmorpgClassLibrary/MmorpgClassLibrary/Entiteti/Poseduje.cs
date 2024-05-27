namespace MmorpgClassLibrary.Entiteti;

internal class Poseduje {
    protected internal virtual int Id { get; set; }
    protected internal virtual Igrac? Igrac { get; set; }
    protected internal virtual Orudje? KljucniPredmet { get; set; }
}