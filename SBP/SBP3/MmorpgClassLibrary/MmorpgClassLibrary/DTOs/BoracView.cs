using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class BoracView : KlasaView {
    public int? KoristiStit { get; set; }
    public int? DualWielder { get; set; }

    public BoracView() {
    }

    internal BoracView(Borac? b) : base(b) {
        if (b == null)
            return;
        KoristiStit = b.KoristiStit;
        DualWielder = b.DualWielder;
    }
}