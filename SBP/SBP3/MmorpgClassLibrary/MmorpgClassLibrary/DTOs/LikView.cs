using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class LikView {
    public int Id { get; set; }
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
        InitRasa(l.Rasa);
        InitKlasa(l.Klasa);
    }

    private void InitRasa(Rasa? r) {
        if (r == null)
            return;
        if (r is Covek covek) {
            Rasa = new CovekView(covek);
        }
        else if (r is Demon demon) {
            Rasa = new DemonView(demon);
        }
        else if (r is Ork ork) {
            Rasa = new OrkView(ork);
        }
        else if (r is Patuljak patuljak) {
            Rasa = new PatuljakView(patuljak);
        }
        else if (r is Vilenjak vilenjak) {
            Rasa = new VilenjakView(vilenjak);
        }
    }

    private void InitKlasa(Klasa? k) {
        if (k == null)
            return;
        if (k is Strelac strelac) {
            Klasa = new StrelacView(strelac);
        }
        else if (k is Borac borac) {
            Klasa = new BoracView(borac);
        }
        else if (k is Oklopnik oklopnik) {
            Klasa = new OklopnikView(oklopnik);
        }
        else if (k is Carobnjak carobnjak) {
            Klasa = new CarobnjakView(carobnjak);
        } 
        else if (k is Svestenik svestenik) {
            Klasa = new SvestenikView(svestenik);
        }
        else if (k is Lopov lopov) {
            Klasa = new LopovView(lopov);
        }
    }
}