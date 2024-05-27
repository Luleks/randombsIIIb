using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class PomocnikView {
    public int Id { get; set; }
    public string? Ime { get; set; }
    public string? Rasa { get; set; }
    public string? Klasa { get; set; }
    public int? BonusZastita { get; set; }
    public IgracView? Igrac { get; set; }

    public PomocnikView() {
    }

    internal PomocnikView(Pomocnik? p) {
        if (p == null)
            return;
        Id = p.Id;
        Ime = p.Ime;
        Rasa = p.Rasa;
        Klasa = p.Klasa;
        BonusZastita = p.BonusZastita;
    }

    internal PomocnikView(Pomocnik? p, Igrac? i) : this(p) {
        Igrac = new IgracView(i);
    }
    
}