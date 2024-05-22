namespace MmorpgClassLibrary.DTOs;

public class PredmetView : OrudjeView {
    public int? DodatniXp { get; set; }
    public bool? KljucniPredmet { get; set; }
    public IList<PosedujeView>? Vlasnici { get; set; }
    public IList<IgraView> NadjenoUIgri { get; set; }
}