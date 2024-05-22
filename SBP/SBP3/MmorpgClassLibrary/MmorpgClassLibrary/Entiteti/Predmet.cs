namespace MmorpgClassLibrary.Entiteti;

internal class Predmet : Orudje {
    protected internal virtual int DodatniXp { get; set; }
    protected internal virtual bool KljucniPredmet { get; set; }
    protected internal virtual IList<Poseduje> Vlasnici { get; set; } = [];
    protected internal virtual IList<Igra> NadjenoUIgri { get; set; } = [];
}