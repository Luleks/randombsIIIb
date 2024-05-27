using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class StazaRestrictionKlasaView {
    public int Id { get; set; }
    public string? Klasa { get; set; }
    public StazaView? Staza { get; set; }

    public StazaRestrictionKlasaView() {
    }

    internal StazaRestrictionKlasaView(StazaRestrictionKlasa? srk) {
        if (srk == null)
            return;
        Id = srk.Id;
        Klasa = srk.Klasa;
    }
    
    internal StazaRestrictionKlasaView(StazaRestrictionKlasa? srk, Staza? s) : this(srk) {
        Staza = new StazaView(s);
    }
    
}