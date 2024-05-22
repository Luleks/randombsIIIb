namespace SBP2.Models.Entiteti;

public abstract class Rasa {
    public virtual int Id { get; set; }
    public virtual Lik? Lik { get; set; }
}