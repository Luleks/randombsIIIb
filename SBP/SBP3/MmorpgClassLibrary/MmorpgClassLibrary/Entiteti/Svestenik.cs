namespace MmorpgClassLibrary.Entiteti;

internal class Svestenik : Klasa {
    protected internal virtual required string Religija { get; set; }
    protected internal virtual required string Blagoslovi { get; set; }
    protected internal virtual int CanHeal { get; set; }
}