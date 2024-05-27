using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class JeKupioView {
    public int Id { get; set; }
    public IgracView? Igrac { get; set; }
    public OrudjeView? ShoppableOrudje { get; set; }
    
    public JeKupioView() {
    }

    internal JeKupioView(JeKupio? j) {
        if (j == null)
            return;
        Id = j.Id;
        Igrac = new IgracView(j.Igrac);
        ShoppableOrudje = new OrudjeView(j.ShoppableOrudje);
    }
}