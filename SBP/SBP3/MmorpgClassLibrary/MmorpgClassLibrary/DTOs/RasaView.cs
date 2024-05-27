using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class RasaView {
    public int Id { get; set; }
    public LikView? Lik { get; set; }

    public RasaView() {
    }

    internal RasaView(Rasa? r) {
        if (r == null)
            return;
        Id = r.Id;
    }

    internal RasaView(Rasa? r, Lik? l) : this(r) {
        Lik = new LikView(l);
    }
}