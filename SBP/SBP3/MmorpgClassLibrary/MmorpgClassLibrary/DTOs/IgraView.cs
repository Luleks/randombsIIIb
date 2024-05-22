namespace MmorpgClassLibrary.DTOs;

public class IgraView {
    public int Id { get; protected set; }
    public DateTime? Vreme { get; set; }
    public GrupaView? Grupa { get; set; }
    public StazaView? Staza { get; set; }
    public OrudjeView? FindableOrudje { get; set; }
}