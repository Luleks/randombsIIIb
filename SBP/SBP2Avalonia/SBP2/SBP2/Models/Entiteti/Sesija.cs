using System;

namespace SBP2.Models.Entiteti;

public class Sesija {
    public virtual int Id { get; protected set; }
    public virtual int Zlato { get; set; }
    public virtual int Xp { get; set; }
    public virtual DateTime Vreme { get; set; }
    public virtual int Duzina { get; set; }
    public virtual required Igrac Igrac { get; set; }
}