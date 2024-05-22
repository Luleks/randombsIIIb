namespace MmorpgClassLibrary.DTOs;

public class GrupaView {
    public int Id { get; protected set; } 
    
    public int? Dummy { get; set; }
    public IList<GroupMembershipView>? Clanovi { get; set; } 
    public IgraView? Igra { get; set; }
}