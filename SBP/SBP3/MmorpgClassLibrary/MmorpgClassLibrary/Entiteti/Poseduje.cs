namespace MmorpgClassLibrary.Entiteti;

internal class Poseduje {
    protected internal virtual int Id { get; set; }
    protected internal virtual required Igrac Igrac { get; set; }
    protected internal virtual required Orudje KljucniPredmet { get; set; }
}