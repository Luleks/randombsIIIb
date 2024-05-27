using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class PatuljakView : RasaView {
    public string? Oruzje { get; set; }

    public PatuljakView() {
    }

    internal PatuljakView(Patuljak? p) : base(p) {
        if (p == null)
            return;
        Oruzje = p.Oruzje;
    }
    
    internal PatuljakView(Patuljak? p, Lik? l) : base(p, l) {
        if (p == null)
            return;
        Oruzje = p.Oruzje;
    }
}