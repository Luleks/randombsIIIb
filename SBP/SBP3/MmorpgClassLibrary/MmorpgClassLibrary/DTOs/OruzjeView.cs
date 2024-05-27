using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class OruzjeView : OrudjeView {
    public int? PoeniZaNapad { get; set; }
    public int? DodatniXp { get; set; }
    public IList<JeKupioView> Kupovine { get; set; } = [];
    public IList<IgraView> NadjenoUIgrni { get; set; } = [];

    public OruzjeView() {
    }

    internal OruzjeView(Oruzje? o) : base(o) {
        if (o == null)
            return;
        PoeniZaNapad = o.PoeniZaNapad;
        DodatniXp = o.DodatniXp;
    }
}