namespace Proj2.Entiteti;

public class OrudjeRestrictionRasa {
    public virtual int Id { get; protected set; }
    public required string Rasa { get; set; }
    public required  Oruzje Oruzje { get; set; }
}