namespace Proj2.Entiteti;

public class OrudjeRestrictionKlasa {
    public virtual int Id { get; protected set; }
    public required string Klasa { get; set; }
    public required  Oruzje Oruzje { get; set; }
}