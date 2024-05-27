using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class SesijaView {
    public int Id { get; set; }
    public int? Zlato { get; set; }
    public int? Xp { get; set; }
    public DateTime? Vreme { get; set; }
    public int? Duzina { get; set; }
    public IgracView? Igrac { get; set; }

    public SesijaView() {
    }

    internal SesijaView(Sesija? s) {
        if (s == null)
            return;
        Id = s.Id;
        Zlato = s.Zlato;
        Xp = s.Xp;
        Vreme = s.Vreme;
        Duzina = s.Duzina;
    }

    internal SesijaView(Sesija? s, Igrac? i) : this(s) {
        Igrac = new IgracView(i);
    }
}