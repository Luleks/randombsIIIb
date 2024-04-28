namespace Proj2.Entiteti;

public abstract class Klasa {
    public virtual int Id { get; protected set; }
    public virtual required Lik Lik { get; set; }
}