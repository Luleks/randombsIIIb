namespace MmorpgClassLibrary.DTOs;

public class SesijaView {
    public int Id { get; protected set; }
    public int? Zlato { get; set; }
    public int? Xp { get; set; }
    public DateTime? Vreme { get; set; }
    public int? Duzina { get; set; }
    public IgracView? Igrac { get; set; }
}