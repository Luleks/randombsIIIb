namespace Proj2.Entiteti;

public class Poseduje {
    public virtual int Id { get; protected set; }
    public virtual required Igrac Igrac { get; set; }
    public virtual required Orudje KljucniPredmet { get; set; }
}