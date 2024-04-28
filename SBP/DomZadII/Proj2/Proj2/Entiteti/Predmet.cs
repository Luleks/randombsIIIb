namespace Proj2.Entiteti;

public class Predmet : Orudje {
    public virtual int DodatniXp { get; set; }
    public virtual bool KljucniPredmet { get; set; }
    public virtual IList<Poseduje> Vlasnici { get; set; } = [];
    public virtual IList<Igra> NadjenoUIgri { get; set; } = [];
}