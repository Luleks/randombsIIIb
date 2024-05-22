namespace MmorpgClassLibrary.DTOs;

public class PomocnikView {
    public int Id { get; set; }
    public string? Ime { get; set; }
    public string? Rasa { get; set; }
    public string? Klasa { get; set; }
    public int? BonusZastita { get; set; }
    public IgracView? Igrac { get; set; }
    
}