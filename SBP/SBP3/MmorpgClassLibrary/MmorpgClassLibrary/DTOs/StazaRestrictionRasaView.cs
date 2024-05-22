namespace MmorpgClassLibrary.DTOs;

public class StazaRestrictionRasaView {
    public int Id { get; protected set; }
    public string? Rasa { get; set; }
    public StazaView? Staza { get; set; }
}