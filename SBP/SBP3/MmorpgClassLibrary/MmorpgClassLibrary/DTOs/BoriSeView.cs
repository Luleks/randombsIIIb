using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class BoriSeView {
    public int Id { get; set; }
    public DateTime? Vreme { get; set; }
    public int? Bonus { get; set; }
    public TimView? Tim1 { get; set; }
    public TimView? Tim2 { get; set; }
    public TimView? Pobednik { get; set; }

    public BoriSeView() {
    }

    internal BoriSeView(BoriSe? b) {
        if (b == null)
            return;
        Id = b.Id;
        Vreme = b.Vreme;
        Bonus = b.Bonus;
        Tim1 = new TimView(b.Tim1);
        Tim2 = new TimView(b.Tim2);
        Pobednik = new TimView(b.Pobednik);
    }
    
}