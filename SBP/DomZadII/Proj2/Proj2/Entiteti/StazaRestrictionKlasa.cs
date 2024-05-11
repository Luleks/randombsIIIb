namespace Proj2.Entiteti;

public class StazaRestrictionKlasa {
    public virtual int Id { get; protected set; }
    public virtual required string Klasa { get; set; }
    public virtual required Staza Staza { get; set; }
}