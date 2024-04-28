namespace Proj2.Entiteti;

public class Svestenik : Klasa {
    public virtual required string Religija { get; set; }
    public virtual required string Blagoslovi { get; set; }
    public virtual bool CanHeal { get; set; }
}