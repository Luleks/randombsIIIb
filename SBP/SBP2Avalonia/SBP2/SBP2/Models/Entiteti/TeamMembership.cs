using System;

namespace SBP2.Models.Entiteti;

public class TeamMembership {
    public virtual int Id { get; protected set; }
    public virtual DateTime VremeOd { get; set; }
    public virtual DateTime? VremeDo { get; set; }
    public virtual required Igrac Igrac { get; set; }
    public virtual required Tim Tim { get; set; }
}