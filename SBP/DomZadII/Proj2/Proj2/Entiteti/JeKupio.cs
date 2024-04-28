namespace Proj2.Entiteti;

public class JeKupio {
    public virtual int Id { get; protected set; }
    public virtual required Igrac Igrac { get; set; }
    public virtual required Orudje ShoppableOrudje { get; set; }
}