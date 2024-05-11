using NHibernate;
using Proj2.Entiteti;

namespace Proj2;

public static class DTOManager {
    public static async Task DodajIgraca(IgracBasic i) {
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                Console.WriteLine("Failed to establish db connection");
                return;
            }

            Igrac igrac = new Igrac {
                Nadimak = i.Nadimak,
                Ime = i.Ime,
                Prezime = i.Prezime,
                Lozinka = i.Lozinka,
                Pol = i.Pol,
                Uzrast = i.Uzrast,
                Lik = null!,
            };

            await s.SaveAsync(igrac);
            await s.FlushAsync();
            
            Lik lik = new Lik() {
                Iskustvo = i.Lik.Iskustvo,
                Igrac = igrac,
                Klasa = null!,
                Rasa = null!,
                NivoZdravlja = i.Lik.NivoZdravlja,
                StepenZamora = i.Lik.StepenZamora,
                Zlato = i.Lik.Zlato
            };

            await s.SaveAsync(lik);
            await s.FlushAsync();
            
            if (i.Lik.Klasa is LopovBasic) {
                Lopov lopov = new Lopov() {
                    Lik = igrac.Lik,
                    NivoBuke = ((LopovBasic)i.Lik.Klasa).NivoBuke,
                    NivoZamki = ((LopovBasic)i.Lik.Klasa).NivoZamki
                };
                await s.SaveAsync(lopov);
                await s.FlushAsync();
            }

            if (i.Lik.Rasa is PatuljakBasic) {
                Patuljak patuljak = new Patuljak() {
                    Lik = igrac.Lik,
                    Oruzje = ((PatuljakBasic)i.Lik.Rasa).TipOruzja,
                };
                await s.SaveAsync(patuljak);
                await s.FlushAsync();
            }

            s.Close();
        }
        catch (Exception ec) {
            Console.WriteLine(ec.Message);
        }
    }
}