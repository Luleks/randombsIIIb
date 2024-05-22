namespace SBP2.Models.Entiteti;

public class Svestenik : Klasa {
    public virtual required string Religija { get; set; }
    public virtual required string Blagoslovi { get; set; }
    public virtual int CanHeal { get; set; }
}