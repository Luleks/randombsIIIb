namespace MmorpgClassLibrary.Entiteti;

internal class Oklop : Orudje {
    protected internal virtual int PoeniZaOdbranu { get; set; }
    protected internal virtual IList<JeKupio> Kupovine { get; set; } = [];
}