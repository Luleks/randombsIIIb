namespace MmorpgClassLibrary.DTOs;

public class TeamMembershipView {
    public int Id { get; protected set; }
    public DateTime? VremeOd { get; set; }
    public DateTime? VremeDo { get; set; }
    public IgracView? Igrac { get; set; }
    public TimView? Tim { get; set; }
}