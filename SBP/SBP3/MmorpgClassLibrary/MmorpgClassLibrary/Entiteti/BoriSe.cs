namespace MmorpgClassLibrary.Entiteti;

internal class BoriSe {
    protected internal virtual int Id { get; set; }
    protected internal virtual DateTime Vreme { get; set; }
    protected internal virtual int Bonus { get; set; }
    protected internal virtual required Tim Tim1 { get; set; }
    protected internal virtual required Tim Tim2 { get; set; }
    protected internal virtual required Tim Pobednik { get; set; }
    
}