namespace Proj2.Entiteti;

public class Pomocnik {
    public virtual int Id { get; protected set; }
    public virtual required string Ime { get; set; }
    public virtual required string Rasa { get; set; }
    public virtual required string Klasa { get; set; }
    public virtual int BonusZastita { get; set; }
    public virtual required Igrac Igrac { get; set; }
    
}