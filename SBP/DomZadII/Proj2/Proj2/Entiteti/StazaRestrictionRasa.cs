namespace Proj2.Entiteti;

public class StazaRestrictionRasa {
    public virtual int Id { get; protected set; }
    public virtual required string Rasa { get; set; }
    public virtual required Staza Staza { get; set; }
}