using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class LopovView : KlasaView {
    public int? NivoZamki { get; set; }
    public int? NivoBuke { get; set; }

    public LopovView() {
    }

    internal LopovView(Lopov? l) : base(l) {
        if (l == null)
            return;
        NivoZamki = l.NivoZamki;
        NivoBuke = l.NivoBuke;
    }
    
    internal LopovView(Lopov? l, Lik? lik) : base(l, lik) {
        if (l == null)
            return;
        NivoZamki = l.NivoZamki;
        l.NivoBuke = l.NivoBuke;
    }
}