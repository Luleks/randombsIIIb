namespace MmorpgClassLibrary.DTOs;

public class BoriSeView {
    public int Id { get; protected set; }
    public DateTime? Vreme { get; set; }
    public int? Bonus { get; set; }
    public TimView? Tim1 { get; set; }
    public TimView? Tim2 { get; set; }
    public TimView? Pobednik { get; set; }
    
}