namespace Proj2.Entiteti;

public class Grupa {
    public virtual int Id { get; protected set; } 
    public virtual IList<GroupMembership> Clanovi { get; set; } = []; 
    public virtual required Igra Igra { get; set; }
}