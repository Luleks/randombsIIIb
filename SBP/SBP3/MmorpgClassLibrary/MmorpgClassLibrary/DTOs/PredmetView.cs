using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class PredmetView : OrudjeView {
    public int? DodatniXp { get; set; }
    public bool? KljucniPredmet { get; set; }
    public IList<PosedujeView>? Vlasnici { get; set; } = [];
    public IList<IgraView> NadjenoUIgri { get; set; } = [];

    public PredmetView() {
    }

    internal PredmetView(Predmet? p) : base(p) {
        if (p == null)
            return;
        DodatniXp = p.DodatniXp;
        KljucniPredmet = p.KljucniPredmet;
    }
}