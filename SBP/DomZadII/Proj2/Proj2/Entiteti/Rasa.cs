namespace Proj2.Entiteti;

public abstract class Rasa {
    public virtual int Id { get; set; }
    public virtual required Lik Lik { get; set; }
}