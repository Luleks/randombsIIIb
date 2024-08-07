using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.DTOs;

public class KlasaView {
    public int Id { get; set; }
    public LikView? Lik { get; set; }

    public KlasaView() {
    }

    internal KlasaView(Klasa? k) {
        if (k == null)
            return;
        Id = k.Id;
    }

    internal KlasaView(Klasa? k, Lik? l) : this(k) {
        Lik = new LikView(l);
    }
}