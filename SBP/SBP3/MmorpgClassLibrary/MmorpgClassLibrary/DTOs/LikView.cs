using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class LikView {
    public int Id { get; protected set; }
    public int? StepenZamora { get; set; }
    public int? Iskustvo { get; set; }
    public int? NivoZdravlja { get; set; }
    public int? Zlato { get; set; }
    public RasaView? Rasa { get; set; }
    public KlasaView? Klasa { get; set; }
    public IgracView? Igrac { get; set; }

    public LikView() {
    }

    internal LikView(Lik? l) {
        if (l == null)
            return;
        Id = l.Id;
        StepenZamora = l.StepenZamora;
        Iskustvo = l.Iskustvo;
        NivoZdravlja = l.NivoZdravlja;
        Zlato = l.Zlato;
    }

    internal LikView(Lik? l, Rasa? r, Klasa? k, Igrac? i) : this(l) {
        
    }
}