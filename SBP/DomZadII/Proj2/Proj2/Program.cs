using NHibernate;

namespace Proj2;

public static class Program {
    public static async Task Main(string[] args) {
        IgracBasic ib = new IgracBasic(12521, "Lule", "123", 'M', 15, "Luka", "Velickovic", null!);
        LikBasic lb = new LikBasic(0, 4, 52, 5, 125, null!, null!);
        KlasaBasic klasa = new LopovBasic(0, 15, 15);
        RasaBasic rasa = new PatuljakBasic(0, "Sekira");
        klasa.Lik = lb;
        rasa.Lik = lb;
        lb.Klasa = klasa;
        lb.Rasa = rasa;
        ib.Lik = lb;
        
        await DTOManager.DodajIgraca(ib);

    }
}

