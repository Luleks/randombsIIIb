namespace MmorpgClassLibrary.DTOs;

public class OruzjeView : OrudjeView {
    public int? PoeniZaNapad { get; set; }
    public int? DodatniXp { get; set; }
    public IList<JeKupioView> Kupovine { get; set; }
    public IList<IgraView> NadjenoUIgrni { get; set; }
}