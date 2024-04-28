using NHibernate;
using Proj2.Entiteti;

namespace Proj2;

public static class Program {
    public static void Main(string[] args) {
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                Console.WriteLine("Failed to open session, please try again");
                return;
            }
            Lik l = new Lik {
                StepenZamora = 5,
                Iskustvo = 50,
                Zlato = 5125,
                NivoZdravlja = 10,
                Rasa = null!,
                Klasa = null!
            };
            Rasa r = new Covek() {
                Lik = l,
                Skrivanje = 10
            };
            Klasa k = new Lopov() {
                Lik = l,
                NivoBuke = 30,
                NivoZamki = 99
            };
            l.Rasa = r;
            l.Klasa = k;
            s.Save(l);
        }
        catch (Exception ec) {
            Console.WriteLine(ec);
        }
        
    }
}

