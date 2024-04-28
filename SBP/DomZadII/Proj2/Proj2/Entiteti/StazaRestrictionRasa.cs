namespace Proj2.Entiteti;

public class StazaRestrictionRasa {
    public virtual int Id { get; protected set; }
    public required string Rasa { get; set; }
    public required Staza Staza { get; set; }
}