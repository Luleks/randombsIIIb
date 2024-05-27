using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class StrelacView : KlasaView {
    public int? LukIliSamostrel { get; set; }

    public StrelacView() {
    }

    internal StrelacView(Strelac? s) : base(s) {
        if (s == null)
            return;
        LukIliSamostrel = s.LukIliSamostrel;
    }
    
    internal StrelacView(Strelac? s, Lik? l) : base(s, l) {
        if (s == null)
            return;
        LukIliSamostrel = s.LukIliSamostrel;
    }
}