namespace MmorpgClassLibrary.DTOs;

public class GroupMembershipView {
    public int Id { get; protected set; }
    public int? PobedjeniNeprijatelji { get; set; }
    public IgracView? Igrac { get; set; }
    public GrupaView? Grupa { get; set; }
}