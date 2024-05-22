namespace MmorpgClassLibrary.DTOs;

public class OrudjeRestrictionKlasaView {
    public int Id { get; protected set; }
    public string? Klasa { get; set; }
    public OruzjeView? Oruzje { get; set; }
}