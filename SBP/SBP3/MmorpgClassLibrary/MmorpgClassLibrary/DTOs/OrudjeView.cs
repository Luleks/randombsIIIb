using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class OrudjeView {
    public int Id { get; set; }
    public string? Naziv { get; set; }
    public string? Opis { get; set; }
    public IList<OrudjeRestrictionRasaView>? OgranicenjaRase { get; set; } = [];
    public IList<OrudjeRestrictionKlasaView>? OgranicenjaKlase { get; set; } = [];

    public OrudjeView() {
    }

    internal OrudjeView(Orudje? o) {
        if (o == null)
            return;
        Id = o.Id;
        Naziv = o.Naziv;
        Opis = o.Opis;
    }
}