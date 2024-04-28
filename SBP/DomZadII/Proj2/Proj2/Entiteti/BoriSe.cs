namespace Proj2.Entiteti;

public class BoriSe {
    public virtual int Id { get; protected set; }
    public virtual DateTime Vreme { get; set; }
    public virtual int Bonus { get; set; }
    public virtual required Tim Tim1 { get; set; }
    public virtual required Tim Tim2 { get; set; }
    public virtual required Tim Pobednik { get; set; }
    
}