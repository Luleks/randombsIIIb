using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class OrudjeRestrictionRasaView {
    public int Id { get; set; }
    public string? Rasa { get; set; }
    public OrudjeView? Orudje { get; set; }

    public OrudjeRestrictionRasaView() {
    }

    internal OrudjeRestrictionRasaView(OrudjeRestrictionRasa? orr) {
        if (orr == null)
            return;
        Id = orr.Id;
        Rasa = orr.Rasa;
    }

    internal OrudjeRestrictionRasaView(OrudjeRestrictionRasa? orr, Orudje? o) : this(orr) {
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