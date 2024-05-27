namespace MmorpgClassLibrary.Entiteti;

internal class Lik {
    protected internal virtual int Id { get; set; }
    protected internal virtual int? StepenZamora { get; set; }
    protected internal virtual int? Iskustvo { get; set; }
    protected internal virtual int? NivoZdravlja { get; set; }
    protected internal virtual int? Zlato { get; set; }
    protected internal virtual Rasa? Rasa { get; set; }
    protected internal virtual Klasa? Klasa { get; set; }
    protected internal virtual Igrac? Igrac { get; set; }
}