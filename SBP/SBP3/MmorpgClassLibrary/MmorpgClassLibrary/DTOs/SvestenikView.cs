using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class SvestenikView : KlasaView {
    public string? Religija { get; set; }
    public string? Blagoslovi { get; set; }
    public int? CanHeal { get; set; }

    public SvestenikView() {
    }

    internal SvestenikView(Svestenik? s) : base(s) {
        if (s == null)
            return;
        Religija = s.Religija;
        Blagoslovi = s.Blagoslovi;
        CanHeal = s.CanHeal;
    }
    
    internal SvestenikView(Svestenik? s, Lik? l) : base(s, l) {
        if (s == null)
            return;
        Religija = s.Religija;
        Blagoslovi = s.Blagoslovi;
        CanHeal = s.CanHeal;
    }
}