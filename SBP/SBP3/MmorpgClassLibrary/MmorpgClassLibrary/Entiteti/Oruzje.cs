namespace MmorpgClassLibrary.Entiteti;

internal class Oruzje : Orudje {
    protected internal virtual int PoeniZaNapad { get; set; }
    protected internal virtual int DodatniXp { get; set; }
    protected internal virtual IList<JeKupio> Kupovine { get; set; } = [];
    protected internal virtual IList<Igra> NadjenoUIgrni { get; set; } = [];
}