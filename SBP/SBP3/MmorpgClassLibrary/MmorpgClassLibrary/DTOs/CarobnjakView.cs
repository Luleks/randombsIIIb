using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class CarobnjakView : KlasaView {
    public string? Magije { get; set; }

    public CarobnjakView() {
    }

    internal CarobnjakView(Carobnjak? c) : base(c) {
        if (c == null)
            return;
        Magije = c.Magije;
    }
    
    internal CarobnjakView(Carobnjak? c, Lik? l) : base(c, l) {
        if (c == null)
            return;
        Magije = c.Magije;
    }
}