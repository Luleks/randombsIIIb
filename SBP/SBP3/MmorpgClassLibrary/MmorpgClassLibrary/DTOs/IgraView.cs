using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class IgraView {
    public int Id { get; set; }
    public DateTime? Vreme { get; set; }
    public GrupaView? Grupa { get; set; }
    public StazaView? Staza { get; set; }
    public OrudjeView? FindableOrudje { get; set; }

    public IgraView() {
    }

    internal IgraView(Igra? i) {
        if (i == null)
            return;
        Id = i.Id;
        Vreme = i.Vreme;
        Grupa = new GrupaView(i.Grupa);
        Staza = new StazaView(i.Staza);

        if (i.FindableOrudje == null)
            return;
        if (i.FindableOrudje is Predmet predmet) {
            FindableOrudje = new PredmetView(predmet);
        }
        else if (i.FindableOrudje is Oruzje oruzje) {
            FindableOrudje = new OruzjeView(oruzje);
        }
    }
}