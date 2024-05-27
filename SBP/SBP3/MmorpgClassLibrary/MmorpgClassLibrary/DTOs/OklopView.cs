using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class OklopView : OrudjeView {
    public int? PoeniZaOdbranu { get; set; }
    public IList<JeKupioView>? Kupovine { get; set; } = [];

    public OklopView() {
    }

    internal OklopView(Oklop? o) : base(o) {
        if (o == null)
            return;
        PoeniZaOdbranu = o.PoeniZaOdbranu;
    }
    
    
}