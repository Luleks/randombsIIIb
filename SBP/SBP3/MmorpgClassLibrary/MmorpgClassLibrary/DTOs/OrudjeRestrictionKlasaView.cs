using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class OrudjeRestrictionKlasaView {
    public int Id { get; set; }
    public string? Klasa { get; set; }
    public OrudjeView? Orudje { get; set; }

    public OrudjeRestrictionKlasaView() {
    }

    internal OrudjeRestrictionKlasaView(OrudjeRestrictionKlasa? ork) {
        if (ork == null)
            return;
        Id = ork.Id;
        Klasa = ork.Klasa;
    }

    internal OrudjeRestrictionKlasaView(OrudjeRestrictionKlasa? ork, Orudje? o) : this(ork) {
        if (o == null)
            return;
        if (o is Predmet predmet) {
            Orudje = new PredmetView(predmet);
        }
        else if (o is Oruzje oruzje) {
            Orudje = new OruzjeView(oruzje);
        }
        else if (o is Oklop oklop) {
            Orudje = new OklopView(oklop);
        }
    }
}