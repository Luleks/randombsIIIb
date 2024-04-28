namespace Proj2.Entiteti;

public class Oruzje : Orudje {
    public virtual int PoeniZaNapad { get; set; }
    public virtual int DodatniXp { get; set; }
    public virtual IList<JeKupio> Kupovine { get; set; } = [];
    public virtual IList<Igra> NadjenoUIgrni { get; set; } = [];
}