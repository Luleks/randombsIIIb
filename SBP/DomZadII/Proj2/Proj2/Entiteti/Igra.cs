namespace Proj2.Entiteti;

public class Igra {
    public virtual int Id { get; protected set; }
    public virtual DateTime Vreme { get; set; }
    public virtual required Grupa Grupa { get; set; }
    public virtual required Staza Staza { get; set; }
    public virtual Orudje? FindableOrudje { get; set; }
}