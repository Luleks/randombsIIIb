using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class CovekView : RasaView {
    public int? Skrivanje { get; set; }

    public CovekView() {
    }

    internal CovekView(Covek? c) : base(c) {
        if (c == null)
            return;
        Skrivanje = c.Skrivanje;
    }

    internal CovekView(Covek? c, Lik? l) : base(c, l) {
        if (c == null)
            return;
        Skrivanje = c.Skrivanje;
    }
}