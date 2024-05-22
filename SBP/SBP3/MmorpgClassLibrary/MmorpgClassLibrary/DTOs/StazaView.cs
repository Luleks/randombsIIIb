namespace MmorpgClassLibrary.DTOs;

public class StazaView {
    public int Id { get; protected set; }
    public string? Naziv { get; set; }
    public int? BonusXp { get; set; }
    public int? TimskaStaza { get; set; } // boolean
    public int? RestrictedStaza { get; set; } // boolan
    public IList<IgraView>? Igranja { get; set; } 
    public IList<StazaRestrictionKlasaView>? OgranicenjaKlase { get; set; }
    public IList<StazaRestrictionRasaView>? OgranicenjaRase { get; set; }
}