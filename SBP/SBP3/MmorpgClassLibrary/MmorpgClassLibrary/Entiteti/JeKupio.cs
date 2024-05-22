namespace MmorpgClassLibrary.Entiteti;

internal class JeKupio {
    protected internal virtual int Id { get; set; }
    protected internal virtual required Igrac Igrac { get; set; }
    protected internal virtual required Orudje ShoppableOrudje { get; set; }
}