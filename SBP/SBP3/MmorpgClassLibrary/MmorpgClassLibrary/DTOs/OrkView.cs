using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class OrkView : RasaView {
    public string? Oruzje { get; set; }

    public OrkView() {
    }

    internal OrkView(Ork? o) : base(o) {
        if (o == null)
            return;
        Oruzje = o.Oruzje;
    }
    
    internal OrkView(Ork? o, Lik? l) : base(o, l) {
        if (o == null)
            return;
        Oruzje = o.Oruzje;
    }
}