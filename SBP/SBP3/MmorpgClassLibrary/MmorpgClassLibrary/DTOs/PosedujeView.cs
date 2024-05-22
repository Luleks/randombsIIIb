namespace MmorpgClassLibrary.DTOs;

public class PosedujeView {
    public int Id { get; protected set; }
    public IgracView? Igrac { get; set; }
    public OrudjeView? KljucniPredmet { get; set; }
}