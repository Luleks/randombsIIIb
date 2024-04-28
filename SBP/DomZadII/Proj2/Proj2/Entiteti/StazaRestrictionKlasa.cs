namespace Proj2.Entiteti;

public class StazaRestrictionKlasa {
    public virtual int Id { get; protected set; }
    public required string Klasa { get; set; }
    public required Staza Staza { get; set; }
}