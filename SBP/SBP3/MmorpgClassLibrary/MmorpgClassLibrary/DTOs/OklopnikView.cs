using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class OklopnikView : KlasaView {
    public int? MaxOklop { get; set; }

    public OklopnikView() {
    }

    internal OklopnikView(Oklopnik? o) : base(o) {
        if (o == null)
            return;
        MaxOklop = o.MaxOklop;
    }
    
    internal OklopnikView(Oklopnik? o, Lik? l) : base(o, l) {
        if (o == null)
            return;
        MaxOklop = o.MaxOklop;
    }
}