using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class StazaRestrictionRasaView {
    public int Id { get; set; }
    public string? Rasa { get; set; }
    public StazaView? Staza { get; set; }

    public StazaRestrictionRasaView() {
    }
    
    internal StazaRestrictionRasaView(StazaRestrictionRasa? srr) {
        if (srr == null)
            return;
        Id = srr.Id;
        Rasa = srr.Rasa;
    }
    
    internal StazaRestrictionRasaView(StazaRestrictionRasa? srr, Staza? s) : this(srr) {
        Staza = new StazaView(s);
    }
}