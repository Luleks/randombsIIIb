namespace SBP2.Models.Entiteti;

public abstract class Klasa {
    public virtual int Id { get; protected set; }
    public virtual Lik? Lik { get; set; }
}