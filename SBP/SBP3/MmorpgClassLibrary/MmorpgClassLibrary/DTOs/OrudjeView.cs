namespace MmorpgClassLibrary.DTOs;

public abstract class OrudjeView {
    public int Id { get; protected set; }
    public string? Naziv { get; set; }
    public string? Opis { get; set; }
    public IList<OrudjeRestrictionRasaView>? OgranicenjaRase { get; set; }
    public IList<OrudjeRestrictionKlasaView>? OgranicenjaKlase { get; set; }
}