namespace Proj2.Entiteti;

public class TeamMembership {
    public virtual int Id { get; set; }
    public virtual DateTime VremeOd { get; set; }
    public virtual required Igrac Igrac { get; set; }
    public virtual required Tim Tim { get; set; }
}