namespace Proj2.Entiteti;

public class GroupMembership {
    public virtual int Id { get; protected set; }
    public virtual int PobedjeniNeprijatelji { get; set; }
    public virtual required Igrac Igrac { get; set; }
    public virtual required Grupa Grupa { get; set; }
}