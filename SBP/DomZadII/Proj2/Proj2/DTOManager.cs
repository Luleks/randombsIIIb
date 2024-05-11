using NHibernate;
using Proj2.Entiteti;

namespace Proj2;

public static class DTOManager {
    public static async void dodajIgraca(IgracBasic i) {
        try {
            Igrac igrac = new Igrac() {
                Nadimak = i.Nadimak,
                Ime = i.Ime,
                Prezime = i.Prezime,
                Lozinka = i.Lozinka,
                Pol = i.Pol,
                Uzrast = i.Uzrast,
                Lik = null!,
            };

            Lik lik = new Lik() {
                Iskustvo = i.Lik.Iskustvo,
                Igrac = null!,
                Klasa = null!,
                Rasa = null!,
                NivoZdravlja = i.Lik.NivoZdravlja,
                StepenZamora = i.Lik.StepenZamora,
                Zlato = i.Lik.Zlato
            };

            if (i.Lik.Klasa is LopovBasic) {
                Lopov lopov = new Lopov() {
                    Lik = null!,
                    NivoBuke = ((LopovBasic)i.Lik.Klasa).NivoBuke,
                    NivoZamki = ((LopovBasic)i.Lik.Klasa).NivoZamki
                };
                // lopov.Lik = lik;
                lik.Klasa = lopov;
            }

            if (i.Lik.Rasa is PatuljakBasic) {
                Patuljak patuljak = new Patuljak() {
                    Lik = null!,
                    Oruzje = ((PatuljakBasic)i.Lik.Rasa).TipOruzja,
                };
                // patuljak.Lik = lik;
                lik.Rasa = patuljak;
            }

            igrac.Lik = lik;
            lik.Igrac = igrac;

            ISession? s = DataLayer.GetSession();

            if (s == null) {
                Console.WriteLine("Failed to establish Db connection");
            }

            await s!.SaveAsync(igrac);
            await s.FlushAsync();
            s.Close();
        }
        catch (Exception ec) {
            Console.WriteLine(ec.Message);
        }
    }
}