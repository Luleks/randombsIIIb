using System.Collections.Generic;

namespace SBP2.Models.Entiteti;

public class Grupa {
    public virtual int Id { get; protected set; } 
    
    public virtual int Dummy { get; set; }
    public virtual IList<GroupMembership> Clanovi { get; set; } = []; 
    public virtual required Igra Igra { get; set; }
}