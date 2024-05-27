using MmorpgClassLibrary.DTOs;
using MmorpgClassLibrary.Entiteti;
using NHibernate;
using NHibernate.Linq;

namespace MmorpgClassLibrary;

public static class DataProvider {

    #region IgracILik
    public static async Task<Result<List<IgracView>, string>> VratiSveIgraceAsync() {
        List<IgracView> igraci;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<IgracView>, string>("Greska prilikom otvaranja sesije", 500);

            igraci = await s.Query<Igrac>().Select(x => new IgracView(x)).ToListAsync();

        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o igracima";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return igraci;
    }
    public static async Task<Result<IgracView, string>> VratiIgracaAsync(int igracId) {
        IgracView? igrac;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<IgracView, string>("Greska prilikom otvaranja sesije", 500);

            igrac = new IgracView(await s.LoadAsync<Igrac>(igracId));
        }
        catch (Exception) {
            return new Result<IgracView, string>("Greska prilikom pribavljanja igraca (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return igrac;
    }
    public static async Task<Result<int, string>> DodajIgracaAsync(IgracView i) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Igrac igrac = new() {
                Ime = i.Ime,
                Prezime = i.Prezime,
                Pol = i.Pol,
                Nadimak = i.Nadimak,
                Lozinka = i.Lozinka,
                Lik = null!,
                Uzrast = i.Uzrast
            };

            await s.SaveAsync(igrac);
            await s.FlushAsync();

            id = igrac.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje igraca (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;
    }
    public static async Task<Result<bool, string>> ObrisiIgracaAsync(int igracId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Igrac igrac = s.Load<Igrac>(igracId);
            Lik? lik = igrac.Lik;
            Klasa? klasa = lik?.Klasa;
            Rasa? rasa = lik?.Rasa;
            
            if (rasa != null)
                await s.DeleteAsync(rasa);
            if (klasa != null)
                await s.DeleteAsync(klasa);
            if (lik != null)
                await s.DeleteAsync(lik);
            await s.DeleteAsync(igrac);

            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajIgracaAsync(IgracView i) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Igrac igrac = await s.LoadAsync<Igrac>(i.Id);
            igrac.Ime = i.Ime;
            igrac.Prezime = i.Prezime;
            igrac.Pol = i.Pol;
            igrac.Lozinka = i.Lozinka;
            igrac.Uzrast = i.Uzrast;

            await s.UpdateAsync(igrac);
            await s.FlushAsync();

            id = igrac.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje igraca (possibly nedozvoljena vrednost atributa ili igrac ne postoji)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;
    }
    
    
    public static async Task<Result<LikView, string>> VratiIgracevogLikaAsync(int igracId) {
        LikView? lik;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<LikView, string>("Greska prilikom otvaranja sesije", 500);

            lik = await s.Query<Lik>().Where(x => x.Igrac!.Id == igracId).Select(x => new LikView(x)).FirstOrDefaultAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o liku";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return lik; 
    }
    public static async Task<Result<int, string>> DodajLikaAsync(int igracId, LikView l) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);
            
            Igrac igrac = await s.GetAsync<Igrac>(igracId);
            if (igrac == null)
                return new Result<int, string>("Dodavanje lika nepostojecem igracu", 403);
            else if (igrac.Lik != null)
                return new Result<int, string>("Dodavanje lika igracu koji vec ima lika", 403);
            
            Lik lik = new() {
                Iskustvo = l.Iskustvo,
                NivoZdravlja = l.NivoZdravlja,
                StepenZamora = l.StepenZamora,
                Zlato = l.Zlato,
                Klasa = null!,
                Rasa = null!,
            };
            igrac.Lik = lik;
            lik.Igrac = igrac;
            
            await s.SaveAsync(lik);
            await s.FlushAsync();

            id = lik.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje lika (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<bool, string>> ObrisiLikaAsync(int likId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Lik l = await s.LoadAsync<Lik>(likId);
            
            if (l.Klasa != null)
                await s.DeleteAsync(l.Klasa);
            if (l.Rasa != null)
                await s.DeleteAsync(l.Rasa);
            await s.DeleteAsync(l);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajLikaAsync(LikView l) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Lik lik = await s.LoadAsync<Lik>(l.Id);
            lik.Iskustvo = l.Iskustvo;
            lik.NivoZdravlja = l.NivoZdravlja;
            lik.Zlato = l.Zlato;
            lik.StepenZamora = l.StepenZamora;
            
            await s.UpdateAsync(lik);
            await s.FlushAsync();

            id = lik.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje lika (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;

    }
    #endregion
 
    #region SlabiEntiteti
    public static async Task<Result<int, string>> DodajSesiju(int igracId, SesijaView sw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Igrac igrac = await s.GetAsync<Igrac>(igracId);
            if (igrac == null)
                return "Dodavanje sesije nepostojecem igracu";

            Sesija sesija = new Sesija() {
                Duzina = sw.Duzina,
                Vreme = sw.Vreme,
                Xp = sw.Xp,
                Zlato = sw.Zlato,
                Igrac = null!,
            };
            sesija.Igrac = igrac;
            
            await s.SaveAsync(sesija);
            await s.FlushAsync();
            id = sesija.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje sesije (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<List<SesijaView>, string>> VratiSveSesijeIgracaAsync(int igracId) {
        List<SesijaView> sesije;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<SesijaView>, string>("Greska prilikom otvaranja sesije", 500);

            sesije = await s.Query<Sesija>().Where(x => x.Igrac!.Id == igracId).Select(x => new SesijaView(x)).ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o sesijama";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return sesije; 
    }
    public static async Task<Result<bool, string>> ObrisiSesijuAsync(int sesijaId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Sesija sesija = await s.LoadAsync<Sesija>(sesijaId);

            await s.DeleteAsync(sesija);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajSesijuAsync(SesijaView sw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Sesija sesija = await s.LoadAsync<Sesija>(sw.Id);
            sesija.Duzina = sw.Duzina;
            sesija.Zlato = sw.Zlato;
            sesija.Vreme = sw.Vreme;
            sesija.Xp = sw.Xp;
            
            await s.UpdateAsync(sesija);
            await s.FlushAsync();
            
            id = sesija.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje sesije (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;

    }
    
    public static async Task<Result<int, string>> DodajPomocnika(int igracId, PomocnikView pw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Igrac igrac = await s.GetAsync<Igrac>(igracId);
            if (igrac == null)
                return "Dodavanje sesije nepostojecem igracu";

            Pomocnik pomocnik = new Pomocnik() {
                BonusZastita = pw.BonusZastita,
                Ime = pw.Ime,
                Klasa = pw.Klasa,
                Rasa = pw.Rasa,
                Igrac = null!
            };
            pomocnik.Igrac = igrac;
            
            await s.SaveAsync(pomocnik);
            await s.FlushAsync();
            id = pomocnik.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje pomocnika (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<List<PomocnikView>, string>> VratiSvePomocnikeIgracaAsync(int igracId) {
        List<PomocnikView> pomocnici;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<PomocnikView>, string>("Greska prilikom otvaranja sesije", 500);

            pomocnici = await s.Query<Pomocnik>().Where(x => x.Igrac!.Id == igracId).Select(x => new PomocnikView(x))
                .ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o pomocnicima";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return pomocnici; 
    }
    public static async Task<Result<bool, string>> ObrisiPomocnikaAsync(int pomocnikId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Pomocnik p = await s.LoadAsync<Pomocnik>(pomocnikId);

            await s.DeleteAsync(p);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajPomocnikaAsync(PomocnikView pw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Pomocnik pomocnik = await s.LoadAsync<Pomocnik>(pw.Id);
            pomocnik.Ime = pw.Ime;
            pomocnik.Rasa = pw.Rasa;
            pomocnik.Klasa = pw.Klasa;
            pomocnik.BonusZastita = pw.BonusZastita;
            
            await s.UpdateAsync(pomocnik);
            await s.FlushAsync();
            
            id = pomocnik.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje pomocnika (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;

    }
  
    #endregion
    
    #region Rase

    public static async Task<Result<int, string>> DodajRasuPatuljakLikuAsync(int likId, PatuljakView pw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Lik lik = await s.GetAsync<Lik>(likId);
            if (lik == null)
                return new Result<int, string>("Dodela rase nepostojecem liku", 403);
            if (lik.Rasa != null)
                return new Result<int, string>("Dodela rase liku koji vec poseduje rasu", 403);

            Patuljak p = new Patuljak() {
                Oruzje = pw.Oruzje,
                Lik = null!,
            };
            lik.Rasa = p;
            p.Lik = lik;
            
            await s.SaveAsync(p);
            await s.FlushAsync();
            id = p.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje rase patuljak (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<PatuljakView, string>> VratiPatuljkaAsync(int patuljakId) {
        PatuljakView patuljak;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<PatuljakView, string>("Greska prilikom otvaranja sesije", 500);

            patuljak = await s.Query<Patuljak>().Where(x => x.Id == patuljakId).Select(x => new PatuljakView(x))
                .FirstOrDefaultAsync();

        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o patuljku";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return patuljak; 
    }
    public static async Task<Result<bool, string>> ObrisiPatuljkaAsync(int patuljakId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Patuljak p = await s.LoadAsync<Patuljak>(patuljakId);

            await s.DeleteAsync(p);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajPatuljkaAsync(PatuljakView p) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Patuljak patuljak = await s.LoadAsync<Patuljak>(p.Id);
            patuljak.Oruzje = p.Oruzje;

            await s.UpdateAsync(patuljak);
            await s.FlushAsync();

            id = patuljak.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje rase patuljak (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;

    }
    
    public static async Task<Result<int, string>> DodajRasuCovekLikuAsync(int likId, CovekView cw)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Lik lik = await s.GetAsync<Lik>(likId);
            if (lik == null)
                return new Result<int, string>("Dodela rase nepostojecem liku", 403);
            if (lik.Rasa != null)
                return new Result<int, string>("Dodela rase liku koji vec poseduje rasu", 403);

            Covek c = new Covek()
            {
                Skrivanje = cw.Skrivanje,
                Lik = null!,
            };
            lik.Rasa = c;
            c.Lik = lik;

            await s.SaveAsync(c);
            await s.FlushAsync();
            id = c.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce dodavanje rase covek (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);
    }
    public static async Task<Result<CovekView, string>> VratiCovekaAsync(int covekId)
    {
        CovekView covek;
        ISession? s = null;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<CovekView, string>("Greska prilikom otvaranja sesije", 500);

            covek = await s.Query<Covek>().Where(x => x.Id == covekId).Select(x => new CovekView(x))
                .FirstOrDefaultAsync();

        }
        catch (Exception)
        {
            return "Doslo je do greske prilikom prikupljanja informacija o coveku";
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return covek;
    }
    public static async Task<Result<bool, string>> ObrisiCovekaAsync(int covekId)
    {
        ISession? s = null;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Covek c = await s.LoadAsync<Covek>(covekId);

            await s.DeleteAsync(c);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajCovekaAsync(CovekView c)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Covek covek = await s.LoadAsync<Covek>(c.Id);
            covek.Skrivanje = c.Skrivanje;

            await s.UpdateAsync(covek);
            await s.FlushAsync();

            id = covek.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce azuriranje rase covek (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return id;
    }

    public static async Task<Result<int, string>> DodajRasuVilenjakLikuAsync(int likId, VilenjakView vw)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Lik lik = await s.GetAsync<Lik>(likId);
            if (lik == null)
                return new Result<int, string>("Dodela rase nepostojecem liku", 403);
            if (lik.Rasa != null)
                return new Result<int, string>("Dodela rase liku koji vec poseduje rasu", 403);

            Vilenjak v = new Vilenjak()
            {
                NivoPotrebneMagije = vw.NivoPotrebneMagije,
                Lik = null!,
            };
            lik.Rasa = v;
            v.Lik = lik;

            await s.SaveAsync(v);
            await s.FlushAsync();
            id = v.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce dodavanje rase vilenjak (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);
    }
    public static async Task<Result<VilenjakView, string>> VratiVilenjakaAsync(int vilenjakId)
    {
        VilenjakView vilenjak;
        ISession? s = null;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<VilenjakView, string>("Greska prilikom otvaranja sesije", 500);

            vilenjak = await s.Query<Vilenjak>().Where(x => x.Id == vilenjakId).Select(x => new VilenjakView(x))
                .FirstOrDefaultAsync();

        }
        catch (Exception)
        {
            return "Doslo je do greske prilikom prikupljanja informacija o vilenjaku";
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return vilenjak;
    }
    public static async Task<Result<bool, string>> ObrisiVilenjakaAsync(int vilenjakId)
    {
        ISession? s = null;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Vilenjak v = await s.LoadAsync<Vilenjak>(vilenjakId);

            await s.DeleteAsync(v);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajVilenjakaAsync(VilenjakView v)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Vilenjak vilenjak = await s.LoadAsync<Vilenjak>(v.Id);
            vilenjak.NivoPotrebneMagije = v.NivoPotrebneMagije;

            await s.UpdateAsync(vilenjak);
            await s.FlushAsync();

            id = vilenjak.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce azuriranje rase vilenjak (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return id;
    }

    public static async Task<Result<int, string>> DodajRasuOrkLikuAsync(int likId, OrkView ow)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Lik lik = await s.GetAsync<Lik>(likId);
            if (lik == null)
                return new Result<int, string>("Dodela rase nepostojecem liku", 403);
            if (lik.Rasa != null)
                return new Result<int, string>("Dodela rase liku koji vec poseduje rasu", 403);


            Ork o = new Ork()
            {
                Oruzje = ow.Oruzje,
                Lik = null!,
            };
            lik.Rasa = o;
            o.Lik = lik;

            await s.SaveAsync(o);
            await s.FlushAsync();
            id = o.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce dodavanje rase ork (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);
    }
    public static async Task<Result<OrkView, string>> VratiOrkaAsync(int orkId)
    {
        OrkView ork;
        ISession? s = null;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<OrkView, string>("Greska prilikom otvaranja sesije", 500);

            ork = await s.Query<Ork>().Where(x => x.Id == orkId).Select(x => new OrkView(x)).FirstOrDefaultAsync();

        }
        catch (Exception)
        {
            return "Doslo je do greske prilikom prikupljanja informacija o orku";
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return ork;
    }
    public static async Task<Result<bool, string>> ObrisiOrkaAsync(int orkId)
    {
        ISession? s = null;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Ork o = await s.LoadAsync<Ork>(orkId);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajOrkaAsync(OrkView o)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Ork ork = await s.LoadAsync<Ork>(o.Id);
            ork.Oruzje = o.Oruzje;

            await s.UpdateAsync(ork);
            await s.FlushAsync();

            id = ork.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce azuriranje rase ork (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return id;
    }
    
    public static async Task<Result<int, string>> DodajRasuDemonLikuAsync(int likId, DemonView dw)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Lik lik = await s.GetAsync<Lik>(likId);
            if (lik == null)
                return new Result<int, string>("Dodela rase nepostojecem liku", 403);
            if (lik.Rasa != null)
                return new Result<int, string>("Dodela rase liku koji vec poseduje rasu", 403);

            Demon d = new Demon()
            {
                NivoPotrebneMagije = dw.NivoPotrebneMagije,
                Lik = null!,
            };
            lik.Rasa = d;
            d.Lik = lik;

            await s.SaveAsync(d);
            await s.FlushAsync();
            id = d.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce dodavanje rase demon (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);
    }
    public static async Task<Result<DemonView, string>> VratiDemonaAsync(int demonId)
    {
        DemonView demon;
        ISession? s = null;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<DemonView, string>("Greska prilikom otvaranja sesije", 500);

            demon = await s.Query<Demon>().Where(x => x.Id == demonId).Select(x => new DemonView(x))
                .FirstOrDefaultAsync();

        }
        catch (Exception)
        {
            return "Doslo je do greske prilikom prikupljanja informacija o demonu";
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return demon;
    }
    public static async Task<Result<bool, string>> ObrisiDemonaAsync(int demonId)
    {
        ISession? s = null;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Demon d = await s.LoadAsync<Demon>(demonId);

            await s.DeleteAsync(d);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajDemonaAsync(DemonView d)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Demon demon = await s.LoadAsync<Demon>(d.Id);
            demon.NivoPotrebneMagije = d.NivoPotrebneMagije;

            await s.UpdateAsync(demon);
            await s.FlushAsync();

            id = demon.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce azuriranje rase demon (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return id;
    }
    #endregion
    
    #region Klase

    public static async Task<Result<int, string>> DodajKlasuLopovLikuAsync(int likId, LopovView lw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);
            
            Lik lik = await s.GetAsync<Lik>(likId);
            if (lik == null)
                return new Result<int, string>("Dodela klase nepostojecem liku", 403);
            if (lik.Klasa != null)
                return new Result<int, string>("Dodela klase liku koji vec poseduje klasu", 500);

            Lopov l = new() {
                NivoBuke = lw.NivoBuke,
                NivoZamki = lw.NivoZamki,
                Lik = null!,
            };
            lik.Klasa = l;
            l.Lik = lik;
            
            await s.SaveAsync(l);
            await s.FlushAsync();
            id = l.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje klase lopov (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<LopovView, string>> VratiLopovaAsync(int lopovId) {
        LopovView lopov;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<LopovView, string>("Greska prilikom otvaranja sesije", 500);

            lopov = await s.Query<Lopov>().Where(x => x.Id == lopovId).Select(x => new LopovView(x))
                .FirstOrDefaultAsync();

        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o lopovu";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return lopov; 
    }
    public static async Task<Result<bool, string>> ObrisiLopovaAsync(int lopovId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Lopov l = await s.LoadAsync<Lopov>(lopovId);

            await s.DeleteAsync(l);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajLopovaAsync(LopovView l) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Lopov lopov = await s.LoadAsync<Lopov>(l.Id);
            lopov.NivoZamki = l.NivoZamki;
            lopov.NivoBuke = l.NivoBuke;
            
            await s.UpdateAsync(lopov);
            await s.FlushAsync();

            id = lopov.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje klase lopov (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;

    }
    
    public static async Task<Result<int, string>> DodajKlasuCarobnjakLikuAsync(int likId, CarobnjakView cw)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Lik lik = await s.GetAsync<Lik>(likId);
            if (lik == null)
                return new Result<int, string>("Dodela klase nepostojecem liku", 403);
            if (lik.Klasa != null)
                return new Result<int, string>("Dodela klase liku koji vec poseduje klasu", 500);
            
            Carobnjak c = new()
            {
                Magije = cw.Magije,
                Lik = null!,
            };
            lik.Klasa = c;
            c.Lik = lik;

            await s.SaveAsync(c);
            await s.FlushAsync();
            id = c.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce dodavanje klase carobnjak (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);
    }
    public static async Task<Result<CarobnjakView, string>> VratiCarobnjakaAsync(int carobnjakId)
    {
        CarobnjakView carobnjak;
        ISession? s = null;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<CarobnjakView, string>("Greska prilikom otvaranja sesije", 500);

            carobnjak = await s.Query<Carobnjak>().Where(x => x.Id == carobnjakId).Select(x => new CarobnjakView(x))
                .FirstOrDefaultAsync();

        }
        catch (Exception)
        {
            return "Doslo je do greske prilikom prikupljanja informacija o carobnjaku";
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return carobnjak;
    }
    public static async Task<Result<bool, string>> ObrisiCarobnjakaAsync(int carobnjakId)
    {
        ISession? s = null;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Carobnjak c = await s.LoadAsync<Carobnjak>(carobnjakId);

            await s.DeleteAsync(c);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajCarobnjakaAsync(CarobnjakView c)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Carobnjak carobnjak = await s.LoadAsync<Carobnjak>(c.Id);
            carobnjak.Magije = c.Magije;
            
            await s.UpdateAsync(carobnjak);
            await s.FlushAsync();

            id = carobnjak.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce azuriranje klase carobnjak (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return id;
    }

    public static async Task<Result<int, string>> DodajKlasuBoracLikuAsync(int likId, BoracView bw)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Lik lik = await s.GetAsync<Lik>(likId);
            if (lik == null)
                return new Result<int, string>("Dodela klase nepostojecem liku", 403);
            if (lik.Klasa != null)
                return new Result<int, string>("Dodela klase liku koji vec poseduje klasu", 500);
            
            Borac b = new()
            {
                KoristiStit = bw.KoristiStit,
                DualWielder = bw.DualWielder,
                Lik = null!,
            };
            lik.Klasa = b;
            b.Lik = lik;

            await s.SaveAsync(b);
            await s.FlushAsync();
            id = b.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce dodavanje klase borac (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);
    }
    public static async Task<Result<BoracView, string>> VratiBorcaAsync(int boracId) {
        BoracView borac;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<BoracView, string>("Greska prilikom otvaranja sesije", 500);

            borac = await s.Query<Borac>().Where(x => x.Id == boracId).Select(x => new BoracView(x))
                .FirstOrDefaultAsync();

        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o borcu";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return borac; 
    }
    public static async Task<Result<bool, string>> ObrisiBorcaAsync(int boracId)
    {
        ISession? s = null;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Borac b = await s.LoadAsync<Borac>(boracId);

            await s.DeleteAsync(b);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajBorcaAsync(BoracView b)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Borac borac = await s.LoadAsync<Borac>(b.Id);
            borac.KoristiStit = b.KoristiStit;
            borac.DualWielder = b.DualWielder;


            await s.UpdateAsync(borac);
            await s.FlushAsync();

            id = borac.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce azuriranje klase borac (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return id;
    }

    public static async Task<Result<int, string>> DodajKlasuSvestenikLikuAsync(int likId, SvestenikView sw)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Lik lik = await s.GetAsync<Lik>(likId);
            if (lik == null)
                return new Result<int, string>("Dodela klase nepostojecem liku", 403);
            if (lik.Klasa != null)
                return new Result<int, string>("Dodela klase liku koji vec poseduje klasu", 500);
            
            Svestenik sv = new()
            {
                Religija = sw.Religija,
                Blagoslovi = sw.Blagoslovi,
                CanHeal = sw.CanHeal,
                Lik = null!,
            };
            lik.Klasa = sv;
            sv.Lik = lik;

            await s.SaveAsync(sv);
            await s.FlushAsync();
            id = sv.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce dodavanje klase svestenik (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);
    }
    public static async Task<Result<SvestenikView, string>> VratiSvestenikaAsync(int svestenikId) {
        SvestenikView svestenik;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<SvestenikView, string>("Greska prilikom otvaranja sesije", 500);

            svestenik = await s.Query<Svestenik>().Where(x => x.Id == svestenikId).Select(x => new SvestenikView(x))
                .FirstOrDefaultAsync();

        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o svesteniku";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return svestenik; 
    }
    public static async Task<Result<bool, string>> ObrisiSvestenikaAsync(int svestenikId)
    {
        ISession? s = null;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Svestenik sv = await s.LoadAsync<Svestenik>(svestenikId);

            await s.DeleteAsync(sv);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajSvestenikaAsync(SvestenikView sv)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Svestenik svestenik = await s.LoadAsync<Svestenik>(sv.Id);
            svestenik.Religija = sv.Religija;
            svestenik.Blagoslovi = sv.Blagoslovi;
            svestenik.CanHeal = sv.CanHeal;


            await s.UpdateAsync(svestenik);
            await s.FlushAsync();

            id = svestenik.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce azuriranje klase svestenik (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return id;
    }

    public static async Task<Result<int, string>> DodajKlasuOklopnikLikuAsync(int likId, OklopnikView ow)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Lik lik = await s.GetAsync<Lik>(likId);
            if (lik == null)
                return new Result<int, string>("Dodela klase nepostojecem liku", 403);
            if (lik.Klasa != null)
                return new Result<int, string>("Dodela klase liku koji vec poseduje klasu", 500);
            
            Oklopnik o = new()
            {
                MaxOklop = ow.MaxOklop,
                Lik = null!,
            };
            lik.Klasa = o;
            o.Lik = lik;

            await s.SaveAsync(o);
            await s.FlushAsync();
            id = o.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce dodavanje klase oklopnik (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);
    }
    public static async Task<Result<OklopnikView, string>> VratiOklopnikaAsync(int oklopnikId) {
        OklopnikView oklopnik;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<OklopnikView, string>("Greska prilikom otvaranja sesije", 500);

            oklopnik = await s.Query<Oklopnik>().Where(x => x.Id == oklopnikId).Select(x => new OklopnikView(x))
                .FirstOrDefaultAsync();

        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o oklopniku";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return oklopnik; 
    }
    public static async Task<Result<bool, string>> ObrisiOklopnikaAsync(int oklopnikId)
    {
        ISession? s = null;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Oklopnik o = await s.LoadAsync<Oklopnik>(oklopnikId);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajOklopnikaAsync(OklopnikView o)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Oklopnik oklopnik = await s.LoadAsync<Oklopnik>(o.Id);
            oklopnik.MaxOklop = o.MaxOklop;


            await s.UpdateAsync(oklopnik);
            await s.FlushAsync();

            id = oklopnik.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce azuriranje klase oklopnik (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return id;
    }

    public static async Task<Result<int, string>> DodajKlasuStrelacLikuAsync(int likId, StrelacView sw)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Lik lik = await s.GetAsync<Lik>(likId);
            if (lik == null)
                return new Result<int, string>("Dodela klase nepostojecem liku", 403);
            if (lik.Klasa != null)
                return new Result<int, string>("Dodela klase liku koji vec poseduje klasu", 500);
            
            Strelac str = new()
            {
                LukIliSamostrel = sw.LukIliSamostrel,
                Lik = null!,
            };
            lik.Klasa = str;
            str.Lik = lik;

            await s.SaveAsync(str);
            await s.FlushAsync();
            id = str.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce dodavanje klase strelac (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);
    }
    public static async Task<Result<StrelacView, string>> VratiStrelcaAsync(int strelacId) {
        StrelacView strelac;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<StrelacView, string>("Greska prilikom otvaranja sesije", 500);

            strelac = await s.Query<Strelac>().Where(x => x.Id == strelacId).Select(x => new StrelacView(x))
                .FirstOrDefaultAsync();

        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o strelcu";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return strelac; 
    }
    public static async Task<Result<bool, string>> ObrisiStrelcaAsync(int strelacId)
    {
        ISession? s = null;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Strelac str = await s.LoadAsync<Strelac>(strelacId);

            await s.DeleteAsync(str);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajStrelcaAsync(StrelacView sv)
    {
        ISession? s = null;
        int id;
        try
        {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Strelac strelac = await s.LoadAsync<Strelac>(sv.Id);
            strelac.LukIliSamostrel = sv.LukIliSamostrel;


            await s.UpdateAsync(strelac);
            await s.FlushAsync();

            id = strelac.Id;
        }
        catch (Exception)
        {
            return new Result<int, string>("Nemoguce azuriranje klase strelac (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return id;
    }

    #endregion
    
    #region Tim
    public static async Task<Result<int, string>> DodajTim(TimView tw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Tim tim = new Tim() {
                Naziv = tw.Naziv,
                BonusXp = tw.BonusXp,
                MinIgraca = tw.MinIgraca,
                MaxIgraca = tw.MaxIgraca,
            };
            
            await s.SaveAsync(tim);
            await s.FlushAsync();
            id = tim.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje tima (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<List<TimView>, string>> VratiSveTimoveAsync() {
        List<TimView> timovi;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<TimView>, string>("Greska prilikom otvaranja sesije", 500);

            timovi = await s.Query<Tim>().Select(x => new TimView(x)).ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o timovima";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return timovi; 
    }
    public static async Task<Result<bool, string>> ObrisiTimAsync(int timId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Tim tim = await s.LoadAsync<Tim>(timId);

            await s.DeleteAsync(tim);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajTimAsync(TimView tw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Tim tim = await s.LoadAsync<Tim>(tw.Id);
            tim.Naziv = tw.Naziv;
            tim.MaxIgraca = tw.MaxIgraca;
            tim.MinIgraca = tw.MinIgraca;
            tim.BonusXp = tw.BonusXp;
            
            await s.UpdateAsync(tim);
            await s.FlushAsync();
            
            id = tim.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje tima (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;

    }
    
    public static async Task<Result<int, string>> DodajBorbu(BoriSeView bsw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            BoriSe bs = new BoriSe() {
                Bonus = bsw.Bonus,
                Vreme = bsw.Vreme,
                Tim1 = s.Load<Tim>(bsw.Tim1?.Id),
                Tim2 = s.Load<Tim>(bsw.Tim2?.Id),
                Pobednik = s.Load<Tim>(bsw.Pobednik?.Id),
            };
            
            await s.SaveAsync(bs);
            await s.FlushAsync();
            id = bs.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje borbe (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<List<BoriSeView>, string>> VratiSveBorbeAsync() {
        List<BoriSeView> borbe;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<BoriSeView>, string>("Greska prilikom otvaranja sesije", 500);

            borbe = await s.Query<BoriSe>().Select(x => new BoriSeView(x)).ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o borbama";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return borbe; 
    }
    public static async Task<Result<bool, string>> ObrisiBorbuAsync(int borbaId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            BoriSe borba = await s.LoadAsync<BoriSe>(borbaId);

            await s.DeleteAsync(borba);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajBorbuAsync(BoriSeView bsw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            BoriSe borba = await s.LoadAsync<BoriSe>(bsw.Id);
            borba.Bonus = bsw.Bonus;
            borba.Vreme = bsw.Vreme;
            
            await s.UpdateAsync(borba);
            await s.FlushAsync();
            
            id = borba.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje borbe (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;

    }
    
    public static async Task<Result<int, string>> DodajClanstvo(TeamMembershipView tmw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            TeamMembership tm = new TeamMembership() {
                Igrac = s.Load<Igrac>(tmw.Igrac?.Id),
                Tim = s.Load<Tim>(tmw.Tim?.Id),
                VremeOd = tmw.VremeOd,
                VremeDo = tmw.VremeDo
            };
            
            await s.SaveAsync(tm);
            await s.FlushAsync();
            id = tm.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje clanstva (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<List<TeamMembershipView>, string>> VratiSveClanoveTimaAsync(int timId) {
        List<TeamMembershipView> clanstva;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<TeamMembershipView>, string>("Greska prilikom otvaranja sesije", 500);

            clanstva = await s.Query<TeamMembership>().Where(x => x.VremeDo == null && x.Tim!.Id == timId)
                .Select(x => new TeamMembershipView(x)).ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o clanstvima";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return clanstva;
    }
    public static async Task<Result<List<TeamMembershipView>, string>> VratiIstorijuTimovaLikaAsync(int igracId) {
        List<TeamMembershipView> clanstva;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<TeamMembershipView>, string>("Greska prilikom otvaranja sesije", 500);

            clanstva = await s.Query<TeamMembership>().Where(x => x.Igrac!.Id == igracId)
                .Select(x => new TeamMembershipView(x)).ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o prethodnim timovima igraca";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return clanstva;
    }
    public static async Task<Result<bool, string>> ObrisiClanstvoAsync(int clanstvoId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            TeamMembership tm = await s.LoadAsync<TeamMembership>(clanstvoId);
            
            await s.DeleteAsync(tm);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajClanstvoAsync(TeamMembershipView tmw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            TeamMembership tm = await s.LoadAsync<TeamMembership>(tmw.Id);
            tm.VremeDo = tmw.VremeDo;
            tm.VremeOd = tmw.VremeOd;
            
            await s.UpdateAsync(tm);
            await s.FlushAsync();
            
            id = tm.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje clanstva (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;

    }
    #endregion
    
    #region Staza
    public static async Task<Result<int, string>> DodajStazu(StazaView sw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Staza staza = new Staza() {
                BonusXp = sw.BonusXp,
                Naziv = sw.Naziv,
                RestrictedStaza = sw.RestrictedStaza,
                TimskaStaza = sw.TimskaStaza
            };
            
            await s.SaveAsync(staza);
            await s.FlushAsync();
            id = staza.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje staze (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<List<StazaView>, string>> VratiSveStazeAsync() {
        List<StazaView> staze;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<StazaView>, string>("Greska prilikom otvaranja sesije", 500);

            staze = await s.Query<Staza>().Select(x => new StazaView(x)).ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o stazama";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return staze; 
    }
    public static async Task<Result<bool, string>> ObrisiStazuAsync(int stazaId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Staza staza = await s.LoadAsync<Staza>(stazaId);

            await s.DeleteAsync(staza);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajStazuAsync(StazaView sw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Staza staza = await s.LoadAsync<Staza>(sw.Id);
            staza.Naziv = sw.Naziv;
            staza.BonusXp = sw.BonusXp;
            staza.TimskaStaza = sw.TimskaStaza;
            staza.RestrictedStaza = sw.RestrictedStaza;
            
            await s.UpdateAsync(staza);
            await s.FlushAsync();
            
            id = staza.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje staze (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;
    }
    
    public static async Task<Result<int, string>> DodajOgranicenjeRaseZaStazu(int stazaId, StazaRestrictionRasaView srrw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            StazaRestrictionRasa srr = new StazaRestrictionRasa() {
                Rasa = srrw.Rasa,
                Staza = s.Load<Staza>(stazaId)
            };
            
            await s.SaveAsync(srr);
            await s.FlushAsync();
            id = srr.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje ogranicenja rase (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);
    }
    public static async Task<Result<List<StazaRestrictionRasaView>, string>> VratiSveOgranicenjaRaseZaStazuAsync(int stazaId) {
        List<StazaRestrictionRasaView> srrws;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<StazaRestrictionRasaView>, string>("Greska prilikom otvaranja sesije", 500);

            srrws = await s.Query<StazaRestrictionRasa>().Where(x => x.Staza!.Id == stazaId)
                .Select(x => new StazaRestrictionRasaView(x)).ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o ogranicenjima rase";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return srrws; 
    }
    public static async Task<Result<bool, string>> ObrisiOgranjicenjeRaseZaStazuAsync(int srrId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            StazaRestrictionRasa srr = await s.LoadAsync<StazaRestrictionRasa>(srrId);

            await s.DeleteAsync(srr);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajOgranicenjeRaseZaStazuAsync(StazaRestrictionRasaView srrw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            StazaRestrictionRasa srr = await s.LoadAsync<StazaRestrictionRasa>(srrw.Id);
            srr.Rasa = srrw.Rasa;
            
            await s.UpdateAsync(srr);
            await s.FlushAsync();
            
            id = srr.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje ogranicenja rase (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;
    }

    public static async Task<Result<int, string>> DodajOgranicenjeKlaseZaStazu(int stazaId, StazaRestrictionKlasaView srkw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            StazaRestrictionKlasa srk = new StazaRestrictionKlasa() {
                Klasa = srkw.Klasa,
                Staza = s.Load<Staza>(stazaId)
            };
            
            await s.SaveAsync(srk);
            await s.FlushAsync();
            id = srk.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje ogranicenja klase (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<List<StazaRestrictionKlasaView>, string>> VratiSveOgranicenjaKlaseZaStazuAsync(int stazaId) {
        List<StazaRestrictionKlasaView> srrks;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<StazaRestrictionKlasaView>, string>("Greska prilikom otvaranja sesije", 500);

            srrks = await s.Query<StazaRestrictionKlasa>().Where(x => x.Staza!.Id == stazaId)
                .Select(x => new StazaRestrictionKlasaView(x)).ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o ogranicenjima klase";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return srrks; 
    }
    public static async Task<Result<bool, string>> ObrisiOgranjicenjeKlaseZaStazuAsync(int srkId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            StazaRestrictionKlasa srk = await s.LoadAsync<StazaRestrictionKlasa>(srkId);

            await s.DeleteAsync(srk);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajOgranicenjeKlaseZaStazuAsync(StazaRestrictionKlasaView srkw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            StazaRestrictionKlasa srk = await s.LoadAsync<StazaRestrictionKlasa>(srkw.Id);
            srk.Klasa = srkw.Klasa;
            
            await s.UpdateAsync(srk);
            await s.FlushAsync();
            
            id = srk.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje ogranicenja klase (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;

    }
    
    #endregion
    
    #region Orudje

    public static async Task<Result<List<OrudjeView>, string>> VratiIgracevInventoryAsync(int igracId) {
        List<OrudjeView> inventory;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<OrudjeView>, string>("Greska prilikom otvaranja sesije", 500);

            inventory = await s.Query<JeKupio>().Where(x => x.Igrac!.Id == igracId)
                .Select(x => new OrudjeView(x.ShoppableOrudje)).ToListAsync();
            inventory.AddRange(await s.Query<Poseduje>().Where(x => x.Igrac!.Id == igracId)
                .Select(x => new OrudjeView(x.KljucniPredmet)).ToListAsync());
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o inventory-ju igraca";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return inventory;        
    }
    public static async Task<Result<int, string>> DodajOruzje(OruzjeView ow) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Oruzje o = new Oruzje() {
                DodatniXp = ow.DodatniXp,
                Opis = ow.Opis,
                PoeniZaNapad = ow.PoeniZaNapad,
                Naziv = ow.Naziv
            };
            
            await s.SaveAsync(o);
            await s.FlushAsync();
            id = o.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje oruzja (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<List<OruzjeView>, string>> VratiSvaOruzjaAsync() {
        List<OruzjeView> oruzja;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<OruzjeView>, string>("Greska prilikom otvaranja sesije", 500);

            oruzja = await s.Query<Oruzje>().Select(x => new OruzjeView(x)).ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o oruzjima";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return oruzja; 
    }
    public static async Task<Result<bool, string>> ObrisiOruzjeAsync(int oruzjeId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Oruzje oruzje = await s.LoadAsync<Oruzje>(oruzjeId);
            
            await s.DeleteAsync(oruzje);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajOruzjeAsync(OruzjeView ow) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Oruzje o = await s.LoadAsync<Oruzje>(ow.Id);
            o.DodatniXp = ow.DodatniXp;
            o.Naziv = ow.Naziv;
            o.PoeniZaNapad = ow.PoeniZaNapad;
            o.Opis = ow.Opis;
            
            await s.UpdateAsync(o);
            await s.FlushAsync();
            
            id = o.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje oruzja (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;

    }
 
    public static async Task<Result<int, string>> DodajOklop(OklopView ow) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Oklop o = new Oklop() {
                Opis = ow.Opis,
                PoeniZaOdbranu = ow.PoeniZaOdbranu,
                Naziv = ow.Naziv
            };
            
            await s.SaveAsync(o);
            await s.FlushAsync();
            id = o.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje oklopa (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<List<OklopView>, string>> VratiSveOklopeAsync() {
        List<OklopView> oklopi;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<OklopView>, string>("Greska prilikom otvaranja sesije", 500);

            oklopi = await s.Query<Oklop>().Select(x => new OklopView(x)).ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o oklopima";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return oklopi; 
    }
    public static async Task<Result<bool, string>> ObrisiOklopAsync(int oklopId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Oklop oklop = await s.LoadAsync<Oklop>(oklopId);
            
            await s.DeleteAsync(oklop);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajOklopAsync(OklopView ow) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Oklop o = await s.LoadAsync<Oklop>(ow.Id);
            o.Opis = ow.Opis;
            o.Naziv = ow.Naziv;
            o.PoeniZaOdbranu = ow.PoeniZaOdbranu;
            
            await s.UpdateAsync(o);
            await s.FlushAsync();
            
            id = o.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje oklopa (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;

    }
    
    public static async Task<Result<int, string>> DodajPredmet(PredmetView pw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Predmet p = new Predmet() {
                DodatniXp = pw.DodatniXp,
                KljucniPredmet = pw.KljucniPredmet,
                Opis = pw.Opis,
                Naziv = pw.Opis,
            };
            
            await s.SaveAsync(p);
            await s.FlushAsync();
            id = p.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje predmeta (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<List<PredmetView>, string>> VratiSvePredmeteAsync() {
        List<PredmetView> predmeti;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<PredmetView>, string>("Greska prilikom otvaranja sesije", 500);

            predmeti = await s.Query<Predmet>().Select(x => new PredmetView(x)).ToListAsync();

        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o predmetima";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return predmeti; 
    }
    public static async Task<Result<bool, string>> ObrisiPredmetAsync(int predmetId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Predmet predmet = await s.LoadAsync<Predmet>(predmetId);
            
            await s.DeleteAsync(predmet);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajPredmetAsync(PredmetView pw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Predmet p = await s.LoadAsync<Predmet>(pw.Id);
            p.Naziv = pw.Naziv;
            p.DodatniXp = pw.DodatniXp;
            p.Opis = pw.Opis;
            p.KljucniPredmet = pw.KljucniPredmet;
            
            await s.UpdateAsync(p);
            await s.FlushAsync();
            
            id = p.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje predmeta (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;

    }

    public static async Task<Result<int, string>> DodajOgranicenjeRaseZaOrudje(int orudjeId, OrudjeRestrictionRasaView orrw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            OrudjeRestrictionRasa orr = new OrudjeRestrictionRasa() {
                Rasa = orrw.Rasa,
                Oruzje = s.Load<Oruzje>(orudjeId)
            };
            
            await s.SaveAsync(orr);
            await s.FlushAsync();
            id = orr.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje ogranicenja rase (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<List<OrudjeRestrictionRasaView>, string>> VratiSveOgranicenjaRaseZaOrudjeAsync(int orudjeId) {
        List<OrudjeRestrictionRasaView> orrws;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<OrudjeRestrictionRasaView>, string>("Greska prilikom otvaranja sesije", 500);

            orrws = await s.Query<OrudjeRestrictionRasa>().Where(x => x.Oruzje!.Id == orudjeId)
                .Select(x => new OrudjeRestrictionRasaView(x)).ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o ogranicenjima rase";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return orrws; 
    }
    public static async Task<Result<bool, string>> ObrisiOgranjicenjeRaseZaOrudjeAsync(int orrId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            OrudjeRestrictionRasa orr = await s.LoadAsync<OrudjeRestrictionRasa>(orrId);

            await s.DeleteAsync(orr);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajOgranicenjeRaseZaOrudjeAsync(OrudjeRestrictionRasaView orrw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            OrudjeRestrictionRasa orr = await s.LoadAsync<OrudjeRestrictionRasa>(orrw.Id);
            orr.Rasa = orrw.Rasa;
            
            await s.UpdateAsync(orr);
            await s.FlushAsync();
            
            id = orr.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje ogranicenja rase (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;

    }

    public static async Task<Result<int, string>> DodajOgranicenjeKlaseZaOrudje(int orudjeId, OrudjeRestrictionKlasaView orkw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            OrudjeRestrictionKlasa ork = new OrudjeRestrictionKlasa() {
                Klasa = orkw.Klasa,
                Oruzje = s.Load<Oruzje>(orudjeId)
            };
            
            await s.SaveAsync(ork);
            await s.FlushAsync();
            id = ork.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje ogranicenja klase (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<List<OrudjeRestrictionKlasaView>, string>> VratiSveOgranicenjaKlaseZaOrudjeAsync(int orudjeId) {
        List<OrudjeRestrictionKlasaView> orkws;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<OrudjeRestrictionKlasaView>, string>("Greska prilikom otvaranja sesije", 500);

            orkws = await s.Query<OrudjeRestrictionKlasa>().Where(x => x.Oruzje!.Id == orudjeId)
                .Select(x => new OrudjeRestrictionKlasaView(x)).ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o ogranicenjima klase";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return orkws; 
    }
    public static async Task<Result<bool, string>> ObrisiOgranjicenjeKlaseZaOrudjeAsync(int orkId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            OrudjeRestrictionKlasa ork = await s.LoadAsync<OrudjeRestrictionKlasa>(orkId);

            await s.DeleteAsync(ork);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    public static async Task<Result<int, string>> AzurirajOgranicenjeKlaseZaOrudjeAsync(OrudjeRestrictionKlasaView orkw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            OrudjeRestrictionKlasa ork = await s.LoadAsync<OrudjeRestrictionKlasa>(orkw.Id);
            ork.Klasa = orkw.Klasa;
            
            await s.UpdateAsync(ork);
            await s.FlushAsync();
            
            id = ork.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce azuriranje ogranicenja klase (possibly nedozvoljena vrednost atributa ili ne postoji instanca)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return id;

    }

    public static async Task<Result<int, string>> DodajKupovinu(JeKupioView jkw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);
            
            Orudje o = await s.LoadAsync<Orudje>(jkw.ShoppableOrudje?.Id);
            Igrac i = s.Load<Igrac>(jkw.Igrac?.Id);
            if (o.OgranicenjaRase!.Count > 0 && o.OgranicenjaRase.All(x => x.Rasa != i.Lik!.Rasa!.GetType().ToString().Split(".").Last().ToUpper()))
                return new Result<int, string>("Nemoguce dodavanje kupovine (igraceva rasa ne moze da ima ovo orudje)", 403);

            if (o.OgranicenjaKlase!.Count > 0 && o.OgranicenjaKlase.All(x => x.Klasa != i.Lik!.Klasa!.GetType().ToString().Split(".").Last().ToUpper()))
                return new Result<int, string>("Nemoguce dodavanje kupovine (igraceva klasa ne moze da ima ovo orudje)", 403);

            JeKupio jk = new JeKupio() {
                Igrac = i,
                ShoppableOrudje = o
            };
            
            await s.SaveAsync(jk);
            await s.FlushAsync();
            id = jk.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje kupovine (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<List<JeKupioView>, string>> VratiSveKupovineIgracaAsync(int igracId) {
        List<JeKupioView> kupovine;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<JeKupioView>, string>("Greska prilikom otvaranja sesije", 500);

            kupovine = await s.Query<JeKupio>().Where(x => x.Igrac!.Id == igracId).Select(x => new JeKupioView(x)).ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o kupovinama";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return kupovine;
    }
    public static async Task<Result<bool, string>> ObrisiKupovinuAsync(int kupovinaId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            JeKupio jk = await s.LoadAsync<JeKupio>(kupovinaId);
            
            await s.DeleteAsync(jk);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    
    public static async Task<Result<int, string>> DodajPosedovanje(PosedujeView pw) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            Poseduje p = new Poseduje() {
                Igrac = s.Load<Igrac>(pw.Igrac?.Id),
                KljucniPredmet = s.Load<Predmet>(pw.KljucniPredmet?.Id)
            };
            
            await s.SaveAsync(p);
            await s.FlushAsync();
            id = p.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje posedovanja (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);

    }
    public static async Task<Result<List<OrudjeView>, string>> VratiSvaPosedovanjaIgracaAsync(int igracId) {
        List<OrudjeView> posedovanja;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<OrudjeView>, string>("Greska prilikom otvaranja sesije", 500);

            posedovanja = await s.Query<Poseduje>().Where(x => x.Igrac!.Id == igracId).Select(x => new OrudjeView(x.KljucniPredmet)).ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o posedovanjima";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return posedovanja;
    }
    public static async Task<Result<bool, string>> ObrisiPosedovanjeAsync(int posedovanjeId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Poseduje p = await s.LoadAsync<Poseduje>(posedovanjeId);
            
            await s.DeleteAsync(p);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    #endregion
    
    #region Igra
    public static async Task<Result<List<GroupMembershipView>, string>> PreuzmiIgracevaIgranjaStaza(int igracId) {
        List<GroupMembershipView> igranjaStaza;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<GroupMembershipView>, string>("Greska prilikom otvaranja sesije", 500);

            igranjaStaza = await s.Query<GroupMembership>().Where(x => x.Igrac!.Id == igracId)
                .Select(x => new GroupMembershipView(x)).ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o sesijama";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return igranjaStaza; 
    }
    public static async Task<Result<List<IgracView>, string>> PreuzmiGrupu(int grupaId) {
        List<IgracView> g;
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<List<IgracView>, string>("Greska prilikom otvaranja sesije", 500);

            g = await s.Query<GroupMembership>().Where(x => x.Grupa!.Id == grupaId)
                .Select(x => new IgracView(x.Igrac!)).ToListAsync();
        }
        catch (Exception) {
            return "Doslo je do greske prilikom prikupljanja informacija o sesijama";
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return g; 
    }
    public static async Task<Result<int, string>> DodajIgranje(IgraView iw, List<int> igraciIds) {
        ISession? s = null;
        int id;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<int, string>("Greska prilikom otvaranja sesije", 500);

            List<Igrac> igraci = await s.Query<Igrac>().Where(x => igraciIds.Contains(x.Id)).ToListAsync();
            if (igraci.Count == 0)
                return new Result<int, string>("Invalid number of identified players (0)", 403);
            if (igraci.Count > 1) {
                bool allSameTeam = igraci.All(x => x.Timovi!.FirstOrDefault(y => y.VremeDo == null) != null &&
                                                   x.Timovi!.FirstOrDefault(y => y.VremeDo == null)!.Tim!.Id ==
                                                   igraci.First().Timovi!.FirstOrDefault(y => y.VremeDo == null)!.Tim!.Id);
                if (!allSameTeam)
                    return new Result<int, string>("Svi igraci moraju biti isti tim", 403);
            }

            Staza staza = await s.LoadAsync<Staza>(iw.Staza?.Id);
            if (staza.TimskaStaza == 1 && igraci.Count < 2)
                return new Result<int, string>("Potrebna su bar 2 igraca za igranje staze", 403);
            List<string?> rase = staza.OgranicenjaRase!.Select(x => x.Rasa).ToList();
            List<string?> klase = staza.OgranicenjaKlase!.Select(x => x.Klasa).ToList();
            if (!rase.All(x => igraci.Count(y => y.Lik!.Rasa!.GetType().ToString().Split(".").Last().ToUpper() == x) >= 1))
                return new Result<int, string>("Nisu prisutne sve neophodne rase za igranje staze", 403);
            if (!klase.All(x => igraci.Count(y => y.Lik!.Rasa!.GetType().ToString().Split(".").Last().ToUpper() == x) >= 1))
                return new Result<int, string>("Nisu prisutne sve neophodne klase za igranje staze", 403);

            Grupa g = new Grupa();

            foreach (var igrac in igraci) {
                var clanstvo = new GroupMembership() {
                    Grupa = g,
                    Igrac = igrac,
                    PobedjeniNeprijatelji = 0
                };
                igrac.Grupe!.Add(clanstvo);
                g.Clanovi!.Add(clanstvo);
            }

            await s.SaveAsync(g);
            await s.FlushAsync();
            
            Orudje find = await s.GetAsync<Orudje>(iw.FindableOrudje?.Id);

            Igra i = new Igra() {
                FindableOrudje = find,
                Grupa = g,
                Vreme = iw.Vreme,
                Staza = staza
            };
            g.Igra = i;
            
            await s.SaveAsync(i);
            await s.FlushAsync();
            id = i.Id;
        }
        catch (Exception) {
            return new Result<int, string>("Nemoguce dodavanje igranja (possibly nedozvoljena vrednost atributa)", 403);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return new Result<int, string>(id, 201);
    }
    public static async Task<Result<bool, string>> ObrisiIgranje(int igraId) {
        ISession? s = null;
        try {
            s = DataLayer.GetSession();

            if (s == null)
                return new Result<bool, string>("Greska prilikom otvaranja sesije", 500);

            Igra i = await s.LoadAsync<Igra>(igraId);
            
            Grupa g = await s.LoadAsync<Grupa>(igraId);

            await s.DeleteAsync(i);
            await s.DeleteAsync(g);
            await s.FlushAsync();
        }
        catch (Exception) {
            return new Result<bool, string>("Nemoguce brisanje (possibly not found)", 404);
        }
        finally {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }
    #endregion
}