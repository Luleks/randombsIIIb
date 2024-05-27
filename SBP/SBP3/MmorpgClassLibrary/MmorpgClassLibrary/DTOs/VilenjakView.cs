using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class VilenjakView : RasaView {
    public int? NivoPotrebneMagije { get; set; }

    public VilenjakView() {
    }

    internal VilenjakView(Vilenjak? v) : base(v) {
        if (v == null)
            return;
        NivoPotrebneMagije = v.NivoPotrebneMagije;
    }
    
    internal VilenjakView(Vilenjak? v, Lik? l) : base(v, l) {
        if (v == null)
            return;
        NivoPotrebneMagije = v.NivoPotrebneMagije;
    }
}