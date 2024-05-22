using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using DynamicData;
using MsBox.Avalonia;
using NHibernate;
using NHibernate.Linq;
using SBP2.Models.Entiteti;

namespace SBP2;

public static class DTOManager {
    private static async Task DodajKlasuLiku(Lik lik, IgracBasic i, ISession s) {
        if (i.Lik.Klasa is LopovBasic) {
            Lopov lopov = new Lopov() {
                NivoBuke = ((LopovBasic)i.Lik.Klasa).NivoBuke,
                NivoZamki = ((LopovBasic)i.Lik.Klasa).NivoZamki
            };
            lopov.Lik = lik;
            lik.Klasa = lopov;
            await s.SaveAsync(lopov);
            await s.FlushAsync();
        }
        else if (i.Lik.Klasa is CarobnjakBasic) {
            Carobnjak carobnjak = new Carobnjak() {
                Magije = ((CarobnjakBasic)i.Lik.Klasa).Magije
            };
            carobnjak.Lik = lik;
            lik.Klasa = carobnjak;
            await s.SaveAsync(carobnjak);
            await s.FlushAsync();
        }
        else if (i.Lik.Klasa is BoracBasic) {
            Borac borac = new Borac() {
                DualWielder = ((BoracBasic)i.Lik.Klasa).DualWilder,
                KoristiStit = ((BoracBasic)i.Lik.Klasa).KoristiStit
            };
            borac.Lik = lik;
            lik.Klasa = borac;
            await s.SaveAsync(borac);
            await s.FlushAsync();
        }
        else if (i.Lik.Klasa is SvestenikBasic) {
            Svestenik svestenik = new Svestenik() {
                Blagoslovi = ((SvestenikBasic)i.Lik.Klasa).Blagoslovi,
                CanHeal = ((SvestenikBasic)i.Lik.Klasa).CanHeal,
                Religija = ((SvestenikBasic)i.Lik.Klasa).Religija,
            };
            svestenik.Lik = lik;
            lik.Klasa = svestenik;
            await s.SaveAsync(svestenik);
            await s.FlushAsync();
        }
        else if (i.Lik.Klasa is OklopnikBasic) {
            Oklopnik oklopnik = new Oklopnik() {
                MaxOklop = ((OklopnikBasic)i.Lik.Klasa).MaxOklop
            };
            oklopnik.Lik = lik;
            lik.Klasa = oklopnik;
            await s.SaveAsync(oklopnik);
            await s.FlushAsync();
        }
        else if (i.Lik.Klasa is StrelacBasic) {
            Strelac strelac = new Strelac() {
                LukIliSamostrel = ((StrelacBasic)i.Lik.Klasa).LukIliSamostrel
            };
            strelac.Lik = lik;
            lik.Klasa = strelac;
            await s.SaveAsync(strelac);
            await s.FlushAsync();
        }
        else {
            throw new Exception("Ambigeoueous klasa");
        }
    }

    private static async Task DodajRasuLiku(Lik lik, IgracBasic i, ISession s) {
        if (i.Lik.Rasa is PatuljakBasic) {
            Patuljak patuljak = new Patuljak() {
                Oruzje = ((PatuljakBasic)i.Lik.Rasa).TipOruzja,
            };
            patuljak.Lik = lik;
            lik.Rasa = patuljak;
            await s.SaveAsync(patuljak);
            await s.FlushAsync();
        }
        else if (i.Lik.Rasa is OrkBasic) {
            Ork ork = new Ork() {
                Oruzje = ((OrkBasic)i.Lik.Rasa).TipOruzja,
            };
            ork.Lik = lik;
            lik.Rasa = ork;
            await s.SaveAsync(ork);
            await s.FlushAsync();
        }
        else if (i.Lik.Rasa is DemonBasic) {
            Demon demon = new Demon() {
                NivoPotrebneMagije = ((DemonBasic)i.Lik.Rasa).NivoPotrebneMagije
            };
            demon.Lik = lik;
            lik.Rasa = demon;
            await s.SaveAsync(demon);
            await s.FlushAsync();
        }
        else if (i.Lik.Rasa is VilenjakBasic) {
            Vilenjak vilenjak = new Vilenjak() {
                NivoPotrebneMagije = ((VilenjakBasic)i.Lik.Rasa).NivoPotrebneMagije
            };
            vilenjak.Lik = lik;
            lik.Rasa = vilenjak;
            await s.SaveAsync(vilenjak);
            await s.FlushAsync();
        }
        else if (i.Lik.Rasa is CovekBasic) {
            Covek covek = new Covek() {
                Skrivanje = ((CovekBasic)i.Lik.Rasa).UspesnostUSkrivanju
            };
            covek.Lik = lik;
            lik.Rasa = covek;
            await s.SaveAsync(covek);
            await s.FlushAsync();
        }
        else {
            throw new Exception("Ambigeoueous rasa");
        }
    }
  
    public static async Task<bool> DodajIgraca(IgracBasic i) {
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return false;
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
                Klasa = null!,
                Rasa = null!,
                NivoZdravlja = i.Lik.NivoZdravlja,
                StepenZamora = i.Lik.StepenZamora,
                Zlato = i.Lik.Zlato
            };
            igrac.Lik = lik;
            lik.Igrac = igrac;

            await s.SaveAsync(lik);
            await s.FlushAsync();

            await DodajKlasuLiku(lik, i, s);
            await DodajRasuLiku(lik, i, s);
            
            s.Close();
            return true;
        }
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return false;
        }
    }

    public static async Task<Igrac?> FindIgracByUsername(string username, string password) {
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return null;
            }

            Igrac igrac = await s.Query<Igrac>().Where(x => x.Nadimak == username && x.Lozinka == password)
                                                .FirstOrDefaultAsync();
            if (igrac == null) {
                await MessageBoxManager.GetMessageBoxStandard("Error", "Wrong username or password").ShowAsync();
                return null;
            }

            s.Close();
            return igrac;
        }
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return null;
        }
    }
    
    public static async Task<bool> UpdateIgrac(Igrac i) {
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return false;
            }
            
            await s.UpdateAsync(i);
            await s.FlushAsync();

            s.Close();
            return true;
        }
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return false;
        }
    }

    public static async Task<bool> ObrisiIgraca(int id) {
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return false;
            }

            Igrac igrac = s.Load<Igrac>(id);
            Lik lik = igrac.Lik;
            Klasa klasa = lik.Klasa;
            Rasa rasa = lik.Rasa;

            await s.DeleteAsync(rasa);
            await s.DeleteAsync(klasa);
            await s.DeleteAsync(lik);
            await s.DeleteAsync(igrac);
            await s.FlushAsync();

            s.Close();
            return true;
        }
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return false;
        }
    }

    public static async Task<Sesija?> DodajSesiju(SesijaBasic sesija) {
        try {
            ISession? session = DataLayer.GetSession();
            if (session == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return null;
            }

            Igrac i = session.Load<Igrac>(sesija.IgracId);

            Sesija s = new Sesija() {
                Duzina = sesija.Duzina,
                Vreme = sesija.Vreme,
                Xp = sesija.Xp,
                Zlato = sesija.Zlato,
                Igrac = i,
            };
            i.Sesije.Add(s);

            await session.UpdateAsync(i);
            await session.FlushAsync();
            session.Close();
            return s;
        }  
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return null;
        }
    }

    public static async Task UpdateSesija(Sesija s) {
        try {
            ISession? session = DataLayer.GetSession();
            if (session == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Session error (safe to ignore)").ShowAsync();
                return;
            }
            
            await session.UpdateAsync(s);
            await session.FlushAsync();
            session.Close();
        }  
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
        }
    }

    public static async Task<List<SesijaPregled>> VratiSesijeIgraca(int igracId) {
        List<SesijaPregled> sesije = [];
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return sesije;
            }

            IEnumerable<Sesija> sveSesije = await s.Query<Sesija>().Where(x => x.Igrac.Id == igracId).
                                                                    OrderByDescending(x => x.Vreme).Take(10).ToListAsync();
            sesije.AddRange(sveSesije.Select(sPrim => new SesijaPregled(sPrim.Vreme, sPrim.Duzina)));

            s.Close();
        }
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return sesije;
        }
        return sesije;
    }

    public static async Task<List<StazaPregled>> VratiSveStaze() {
        List<StazaPregled> staze = [];
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return staze;
            }

            IEnumerable<Staza> sveStaze = await s.Query<Staza>().ToListAsync();
            
            staze.AddRange(sveStaze.Select(x => new StazaPregled(x.Id, x.Naziv, x.BonusXp, x.TimskaStaza,
                x.OgranicenjaRase.Select(y => y.Rasa).ToList(), x.OgranicenjaKlase.Select(y => y.Klasa).ToList())));
            
            s.Close();
        }
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return staze;
        }
        return staze;
    }
    
    public static async Task<Pomocnik?> DodajPomocnika(PomocnikBasic pomocnik) {
        try {
            ISession? session = DataLayer.GetSession();
            if (session == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return null;
            }

            Igrac i = session.Load<Igrac>(pomocnik.IgracId);

            Pomocnik p = new Pomocnik() {
                BonusZastita = pomocnik.BonusZastita,
                Ime = pomocnik.Ime,
                Klasa = pomocnik.Klasa,
                Rasa = pomocnik.Rasa,
                Igrac = i
            };
            i.Pomocnici.Add(p);
                
            await session.UpdateAsync(i);
            await session.FlushAsync();
            session.Close();
            return p;
        }  
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return null;
        }
    }

    public static async Task<bool> UpdatePomocnik(Pomocnik pomocnik) {
        try {
            ISession? session = DataLayer.GetSession();
            if (session == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Session error (safe to ignore)").ShowAsync();
                return false;
            }
            
            await session.UpdateAsync(pomocnik);
            await session.FlushAsync();
            session.Close();
            await MessageBoxManager.GetMessageBoxStandard("Success", "Uspesno izmenjen pomocnik").ShowAsync();
            return true;
        }  
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return false;
        }
    }

    public static async Task<bool> DeletePomocnik(Pomocnik pomocnik) {
        try {
            ISession? session = DataLayer.GetSession();
            if (session == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Session error (safe to ignore)").ShowAsync();
                return false;
            }
            
            await session.DeleteAsync(pomocnik);
            await session.FlushAsync();
            session.Close();
            await MessageBoxManager.GetMessageBoxStandard("Success", "Uspesno obrisan pomocnik").ShowAsync();
            return true;
        }  
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return false;
        }
    }

    public static async Task<List<PomocnikPregled>> VratiPomocnikeIgraca(int igracId) {
        List<PomocnikPregled> pomocnici = [];
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return pomocnici;
            }

            IEnumerable<Pomocnik> sviPomocnici = await s.Query<Pomocnik>().Where(x => x.Igrac.Id == igracId).ToListAsync();
            pomocnici.AddRange(sviPomocnici.Select(x => new PomocnikPregled(x.Id, x.Ime, x.Rasa, x.Klasa, x.BonusZastita)));

            s.Close();
            return pomocnici;
        }
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return pomocnici;
        }
    }

    public static async Task<TimPregled?> VratiTimIgraca(int igracId) {
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return null;
            }

            Igrac igrac = await s.LoadAsync<Igrac>(igracId);

            TeamMembership? tm = igrac.Timovi.FirstOrDefault(x => x.VremeDo == null);
            if (tm == null)
                return new TimPregled(-1, "", 0, 0, DateTime.Now, 0, 0, 0);

            Tim tim = tm.Tim;
            int brClanova = tim.Clanovi.Count(x => x.Tim.Id == tim.Id && x.VremeDo == null);

            int brojPobeda = tim.Pobede.Count();
            int brojBorbi = tim.HomeBorbe.Count() + tim.GuestBorbe.Count();
            TimPregled tp = new TimPregled(tim.Id, tim.Naziv, tim.MaxIgraca, tim.BonusXp, tm.VremeOd, brClanova, brojPobeda,
                brojBorbi - brojPobeda);

            s.Close();
            
            return tp;
        }
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return null;
        }
    }

    public static async Task<TimPregled?> NapraviTim(TimBasic tim) {
        try {
            ISession? session = DataLayer.GetSession();
            if (session == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection")
                    .ShowAsync();
                return null;
            }

            Igrac i = session.Load<Igrac>(tim.IgracId);

            Tim t = new Tim() {
                BonusXp = tim.BonusXp,
                MaxIgraca = tim.MaxIgraca,
                MinIgraca = 0,
                Naziv = tim.Ime,
            };
            
            TeamMembership tm = new TeamMembership() {
                Igrac = i,
                Tim = t,
                VremeOd = DateTime.Now
            };
            
            t.Clanovi.Add(tm);

            TeamMembership? oldTeam = i.Timovi.FirstOrDefault(x => x.VremeDo == null);
            if (oldTeam != null) {
                oldTeam.VremeDo = DateTime.Now;
            }

            i.Timovi.Add(tm);

            await session.SaveAsync(t);
            await session.UpdateAsync(i);
            await session.FlushAsync();
            session.Close();
            return new TimPregled(t.Id, t.Naziv, t.MaxIgraca, t.BonusXp, tm.VremeOd, 1, 0, 0);
        }
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return null;
        }
    }

    public static async Task<bool> NapustiTim(int timId, int igracId) {
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return false;
            }

            IQuery q = s.CreateQuery("select t from TeamMembership t where t.Igrac.Id = :igrac and t.Tim.Id = :tim and t.VremeDo = null");
            q.SetInt32("igrac", igracId);
            q.SetInt32("tim", timId);

            TeamMembership t = q.UniqueResult<TeamMembership>();

            if (t == null)
                return false;

            t.VremeDo = DateTime.Now;

            await s.UpdateAsync(t);
            await s.FlushAsync();
            s.Close();
            return true;
        }
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return false;
        }
    }
    
    public static async Task<int> NovaBorba(int tim1Id, string tim2Ime) {
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return 0;
            }

            Tim tim1 = await s.LoadAsync<Tim>(tim1Id);
            Tim tim2 = await s.Query<Tim>().Where(x => x.Naziv == tim2Ime).FirstOrDefaultAsync();

            if (tim1 == null) {
                await MessageBoxManager.GetMessageBoxStandard("Greska", "Error").ShowAsync();
                return 0;
            }

            if (tim2 == null || tim2.Clanovi.Count(x => x.VremeDo == null) == 0) {
                await MessageBoxManager.GetMessageBoxStandard("Greska", "Ne postoji tim sa tim imenom ili tim nema igraca").ShowAsync();
                return 0;
            }

            Random rnd = new Random();
            int pobednik = rnd.Next(0, 2);
            
            BoriSe novaBorba = new BoriSe() {
                Bonus = rnd.Next(1, 900),
                Vreme = DateTime.Now,
                Tim1 = tim1,
                Tim2 = tim2,
                Pobednik = pobednik == 0 ? tim1 : tim2
            };

            await s.SaveAsync(novaBorba);
            await s.FlushAsync();

            s.Close();
            return pobednik == 0 ? 1 : -1;
        }
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return 0;
        }
    }
    
    public static async Task<TimPregled?> PridruziSeTimu(string naziv, int igracId) {
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return null;
            }

            Tim t = await s.Query<Tim>().Where(x => x.Naziv == naziv).FirstOrDefaultAsync();
            int count;
            if (t == null || ((count = t.Clanovi.Count(x => x.VremeDo == null)) == t.MaxIgraca)) {
                return null;
            }

            Igrac i = await s.LoadAsync<Igrac>(igracId);
            
            TeamMembership? oldTim = await s.Query<TeamMembership>().Where(x => x.Igrac.Id == igracId && x.VremeDo == null)
                .FirstOrDefaultAsync();

            if (oldTim != null && oldTim.Tim.Naziv == naziv) {
                await MessageBoxManager.GetMessageBoxStandard("Greska", "Nije moguce pridruziti se istom timu").ShowAsync();
                return null;
            }
            if (oldTim != null) {
                oldTim.VremeDo = DateTime.Now;
                await s.UpdateAsync(oldTim);
            }

            TeamMembership newMembership = new TeamMembership() {
                Igrac = i,
                VremeOd = DateTime.Now,
                Tim = t,
            };
            i.Timovi.Add(newMembership);
            t.Clanovi.Add(newMembership);

            int brojPobeda = t.Pobede.Count();
            int brojBorbi = t.HomeBorbe.Count() + t.GuestBorbe.Count();
            
            await s.SaveAsync(newMembership);
            await s.FlushAsync();
            s.Close();
            return new TimPregled(t.Id, t.Naziv, t.MaxIgraca, t.BonusXp, newMembership.VremeOd, count + 1, brojPobeda,
                brojBorbi - brojPobeda);
        }
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return null;
        } 
    }

    public static async Task<List<ClanTimaPregled>> VratiClanoveIgracevogTima(int igracId) {
        List<ClanTimaPregled> clanovi = [];
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return clanovi;
            }

            TeamMembership tim = await s.Query<TeamMembership>().Where(x => x.Igrac.Id == igracId && x.VremeDo == null)
                .FirstOrDefaultAsync();
            if (tim == null) {
                return [];
            }

            IEnumerable<TeamMembership> sviClanovi = await s.Query<TeamMembership>()
                .Where(x => x.VremeDo == null && x.Tim.Id == tim.Tim.Id).ToListAsync();

            clanovi.AddRange(sviClanovi.Select(x =>
                new ClanTimaPregled(x.Igrac.Id, x.Igrac.Nadimak, x.Igrac.Lik.Rasa.GetType().ToString().Split(".").Last(),
                    x.Igrac.Lik.Klasa.GetType().ToString().Split(".").Last())).ToList());
            
            s.Close();
        }
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return clanovi;
        }
        return clanovi;
    }

    public static async Task<LeaderboardsPregled?> VratiLeaderboards() {
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return null;
            }

            var teamsWithMostWinsPregleds = await s.Query<Tim>()
                .Where(x => x.Pobede.Any())
                .OrderByDescending(x => x.Pobede.Count())
                .Select(x => new TeamWithMostWinsPregled(x.Naziv, x.Pobede.Count())).Take(10).ToListAsync();
            
            
            var playersWithMostGold = await s.Query<Igrac>()
                .OrderByDescending(x => x.Lik.Zlato + x.Sesije.Sum(y => y.Zlato))
                .Select(x => new PlayerWithMostGold(x.Nadimak, x.Lik.Zlato + x.Sesije.Sum(y => y.Zlato)))
                .Take(10)
                .ToListAsync();
            
            var playersWithMostXp = await s.Query<Igrac>()
                .OrderByDescending(x => x.Lik.Iskustvo + x.Sesije.Sum(y => y.Xp) + x.Timovi.Sum(z => z.Tim.BonusXp))
                .Select(x => new PlayerWithMostXp(x.Nadimak, x.Lik.Iskustvo + x.Sesije.Sum(y => y.Xp) + x.Timovi.Sum(z => z.Tim.BonusXp))).Take(10).ToListAsync();
            
            var teamsWithHighestPercentage = await s.Query<Tim>()
                .Where(x => x.HomeBorbe.Any() || x.GuestBorbe.Any() )
                .OrderByDescending(x => (float)x.Pobede.Count() / (x.HomeBorbe.Count() + x.GuestBorbe.Count()))
                .Select(x =>
                    new TeamWithHighestWinPercentage(x.Naziv,
                        (float)x.Pobede.Count() * 100 / (x.HomeBorbe.Count() + x.GuestBorbe.Count())))
                .Take(10).ToListAsync();

            if (teamsWithMostWinsPregleds == null || teamsWithHighestPercentage == null ||
                playersWithMostGold == null || playersWithMostXp == null) {
                await MessageBoxManager.GetMessageBoxStandard("Greska", "Doslo je do greske prilikom ucitavanja leaderboardsa").ShowAsync();
                return null;
            }

            return new LeaderboardsPregled(teamsWithMostWinsPregleds, teamsWithHighestPercentage,
                playersWithMostGold, playersWithMostXp);
        }
        
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return null;
        }
    }
    
    public static async Task<List<ShoppableOrudjePregled>> VratiShoppableOrudje() {
        List<ShoppableOrudjePregled> ret = [];
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return ret;
            }

            Random rnd = new Random();

            ret = await s.Query<Orudje>().Where(x => x is Oruzje || x is Oklop)
                .Select(x => new ShoppableOrudjePregled(x.Id, x.Naziv, x.Opis)).ToListAsync();
            
            s.Close();
        }
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return ret;
        }
        return ret;
    }

    public static async Task KupiOrudje(int igracId, int orudjeId) {
        try {
            ISession? session = DataLayer.GetSession();
            if (session == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Session error (safe to ignore)").ShowAsync();
                return;
            }

            var igrac = await session.LoadAsync<Igrac>(igracId);
            var orudje = await session.LoadAsync<Orudje>(orudjeId);

            if (igrac == null || orudje == null) {
                await MessageBoxManager.GetMessageBoxStandard("Greska", "Neocekivana greska, kontaktirajte support").ShowAsync();
                return;
            }
            if (orudje.OgranicenjaRase.Count > 0 && orudje.OgranicenjaRase.All(x => x.Rasa != igrac.Lik.Rasa.GetType().ToString().Split(".").Last().ToUpper())) {
                await MessageBoxManager.GetMessageBoxStandard("Greska", "Vasa rasa ne moze da ima ovo orudje").ShowAsync();
                return;
            }

            if (orudje.OgranicenjaKlase.Count > 0 && orudje.OgranicenjaKlase.All(x => x.Klasa != igrac.Lik.Klasa.GetType().ToString().Split(".").Last().ToUpper())) {
                await MessageBoxManager.GetMessageBoxStandard("Greska", "Vasa klasa ne moze da ima ovo orudje")
                    .ShowAsync();
                return;
            }

            JeKupio kupovina =
                await session.Query<JeKupio>().Where(x => x.Igrac == igrac && x.ShoppableOrudje == orudje)
                    .FirstOrDefaultAsync(); 

            if (kupovina != null) {
                await MessageBoxManager.GetMessageBoxStandard("Greska", "Vec posedujete ovo orudje")
                    .ShowAsync();
                return;
            } 

            var novaKupovina = new JeKupio() {
                Igrac = igrac,
                ShoppableOrudje = orudje
            };

            await session.SaveAsync(novaKupovina);
            await session.FlushAsync();
            
            session.Close();
            await MessageBoxManager.GetMessageBoxStandard("Success", "Uspesno obavljena kupovina").ShowAsync();
            return;
        }  
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return;
        }
    }

    public static async Task<List<OrudjePregled>> VratiIgracevInventory(int igracId) {
        List<OrudjePregled> orudja = [];
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Failed to establish db connection").ShowAsync();
                return orudja;
            }

            var shoppedItems = await s.Query<JeKupio>().Where(x => x.Igrac.Id == igracId).Select(x =>
                new OrudjePregled(x.ShoppableOrudje.Id, x.ShoppableOrudje.Naziv, x.ShoppableOrudje.Opis,
                    x.ShoppableOrudje.GetType().ToString())).ToListAsync();
            var foundItems = await s.Query<Igra>().Where(x => x.Grupa.Clanovi.Any(y => y.Igrac.Id == igracId) && x.FindableOrudje != null)
                .Select(x => new OrudjePregled(x.FindableOrudje!.Id, x.FindableOrudje.Naziv, x.FindableOrudje.Opis, x.FindableOrudje.GetType().ToString()))
                .ToListAsync();
            
            orudja.AddRange(shoppedItems);
            orudja.AddRange(foundItems);
            
            s.Close();
        }
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return orudja;
        }
        return orudja;
    }

    public static async Task<bool> ProdajOruzje(int igracId, int oruzjeId) {
        try {
            ISession? session = DataLayer.GetSession();
            if (session == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Session error (safe to ignore)").ShowAsync();
                return false;
            }

            var kupovina = await session.Query<JeKupio>()
                .Where(x => x.Igrac.Id == igracId && x.ShoppableOrudje.Id == oruzjeId).FirstOrDefaultAsync();
            if (kupovina == null) {
                await MessageBoxManager.GetMessageBoxStandard("Greska", "Prodaja nepostojeceg itema").ShowAsync();
                return false;
            }
            
            await session.DeleteAsync(kupovina);
            await session.FlushAsync();
            session.Close();
            await MessageBoxManager.GetMessageBoxStandard("Success", "Uspesno prodato oruzje").ShowAsync();
            return true;
        }  
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
            return false;
        }
    }

    public static async Task IgranjeStaze(int stazaId, List<int> igraciId) {
        try {
            ISession? session = DataLayer.GetSession();
            if (session == null) {
                await MessageBoxManager.GetMessageBoxStandard("Failed connection", "Session error (safe to ignore)").ShowAsync();
                return;
            }

            List<Igrac> igraci = await session.Query<Igrac>().Where(x => igraciId.Contains(x.Id)).ToListAsync();
            var staza = await session.LoadAsync<Staza>(stazaId);
            if (igraci.Count != igraciId.Count) {
                await MessageBoxManager.GetMessageBoxStandard("Greska", "Greska prilikom ucitavanja igraca").ShowAsync();
                return;
            }

            if (staza == null) {
                await MessageBoxManager.GetMessageBoxStandard("Greska", "Greska prilikom ucitavanja staze").ShowAsync();
                return;
            }

            var grupa = new Grupa() {
                Igra = null!
            };

            Random rnd = new Random();

            foreach (var i in igraci) {
                var clanstvo = new GroupMembership() {
                    Grupa = grupa,
                    Igrac = i,
                    PobedjeniNeprijatelji = rnd.Next(0, 100)
                };
                i.Grupe.Add(clanstvo);
                grupa.Clanovi.Add(clanstvo);
            }

            await session.SaveAsync(grupa);
            await session.FlushAsync();
            
            bool findableReward = rnd.Next(1, 4) == 1;
            List<Orudje>? o = null;
            if (findableReward) {
                o = await session.Query<Orudje>()
                    .Where(x => x is Predmet || x is Oruzje).ToListAsync();
            }

            if (o == null) findableReward = false;
            var findableOrudje = findableReward ? o![rnd.Next(0, o.Count - 1)] : null;

            var igra = new Igra() {
                FindableOrudje = findableOrudje,
                Grupa = grupa,
                Staza = staza,
                Vreme = DateTime.Now
            };
            grupa.Igra = igra;
            await session.SaveAsync(igra);
            await session.FlushAsync();
            session.Close();
            string tekst = findableOrudje != null ? $"Osvojili ste {findableOrudje.Naziv}" : "Niste osvojili nagradu";
            await MessageBoxManager.GetMessageBoxStandard("Uspeh", tekst).ShowAsync();
        }  
        catch (Exception ec) {
            await MessageBoxManager.GetMessageBoxStandard("Error", ec.FormatExceptionMessage()).ShowAsync();
        }
    }
}