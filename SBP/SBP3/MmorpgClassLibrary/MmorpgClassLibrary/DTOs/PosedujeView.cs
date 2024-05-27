using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class PosedujeView {
    public int Id { get; set; }
    public IgracView? Igrac { get; set; }
    public OrudjeView? KljucniPredmet { get; set; }

    public PosedujeView() {
    }

    internal PosedujeView(Poseduje? p) {
        if (p == null)
            return;
        Id = p.Id;
        Igrac = new IgracView(p.Igrac);
        KljucniPredmet = new OrudjeView(p.KljucniPredmet);
    }
}