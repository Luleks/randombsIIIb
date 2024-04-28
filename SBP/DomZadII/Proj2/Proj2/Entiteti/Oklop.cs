namespace Proj2.Entiteti;

public class Oklop : Orudje {
    public virtual int PoeniZaOdbranu { get; set; }
    public virtual IList<JeKupio> Kupovine { get; set; } = [];
}