namespace Proj2.Entiteti;

public class OrudjeRestrictionRasa {
    public virtual int Id { get; protected set; }
    public virtual required string Rasa { get; set; }
    public virtual required Oruzje Oruzje { get; set; }
}