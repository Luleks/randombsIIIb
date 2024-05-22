namespace SBP2.Models.Entiteti;

public class Lik {
    public virtual int Id { get; protected set; }
    public virtual int StepenZamora { get; set; }
    public virtual int Iskustvo { get; set; }
    public virtual int NivoZdravlja { get; set; }
    public virtual int Zlato { get; set; }
    public virtual required Rasa Rasa { get; set; }
    public virtual required Klasa Klasa { get; set; }
    public virtual Igrac? Igrac { get; set; }
}