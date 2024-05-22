namespace SBP2.Models.Entiteti;

public class OrudjeRestrictionKlasa {
    public virtual int Id { get; protected set; }
    public virtual required string Klasa { get; set; }
    public virtual required Oruzje Oruzje { get; set; }
}