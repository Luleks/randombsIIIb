namespace MmorpgClassLibrary.DTOs;

public class StazaRestrictionKlasaView {
    public int Id { get; protected set; }
    public string? Klasa { get; set; }
    public StazaView? Staza { get; set; }
}