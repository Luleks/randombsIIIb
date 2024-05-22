namespace MmorpgClassLibrary.DTOs;

public class OrudjeRestrictionRasaView {
    public int Id { get; protected set; }
    public string? Rasa { get; set; }
    public OruzjeView? Oruzje { get; set; }
}