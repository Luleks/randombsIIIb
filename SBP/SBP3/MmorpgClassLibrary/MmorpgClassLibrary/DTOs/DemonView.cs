using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class DemonView : RasaView {
    public int? NivoPotrebneMagije { get; set; }

    public DemonView() {
    }

    internal DemonView(Demon? d) : base(d) {
        if (d == null)
            return;
        NivoPotrebneMagije = d.NivoPotrebneMagije;
    }
    
    internal DemonView(Demon? d, Lik? l) : base(d, l) {
        if (d == null)
            return;
        NivoPotrebneMagije = d.NivoPotrebneMagije;
    }
}