namespace MmorpgClassLibrary.DTOs;

public class OklopView : OrudjeView {
    public int? PoeniZaOdbranu { get; set; }
    public IList<JeKupioView>? Kupovine { get; set; }
}