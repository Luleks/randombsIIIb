using System;
using System.Collections.Generic;
using System.Linq;

namespace SBP2;

public class IgracBasic(
    int igId,
    string nadimak,
    string lozinka,
    char pol,
    int uzrast,
    string ime,
    string? prezime,
    LikBasic lik) {
    public int Id { get; set; } = igId;
    public string Nadimak { get; set; } = nadimak;
    public string Lozinka { get; set; } = lozinka;
    public char Pol { get; set; } = pol;
    public  string Ime { get; set; } = ime;
    public string? Prezime { get; set; } = prezime;
    public int Uzrast { get; set; } = uzrast;
    public LikBasic Lik { get; set; } = lik;
}

public class LikBasic(
    int likId,
    int stepenZamora,
    int iskustvo,
    int nivoZdravlja,
    int zlato,
    KlasaBasic klasa,
    RasaBasic rasa) {
    public int Id { get; set; } = likId;
    public int StepenZamora { get; set; } = stepenZamora;
    public int Iskustvo { get; set; } = iskustvo;
    public int NivoZdravlja { get; set; } = nivoZdravlja;
    public int Zlato { get; set; } = zlato;
    public IgracBasic Igrac { get; set; } = null!;
    public KlasaBasic Klasa { get; set; } = klasa;
    public RasaBasic Rasa { get; set; } = rasa;
}

#region Klase
public abstract class KlasaBasic(int id) {
    public int Id { get; set; } = id;
    public LikBasic Lik { get; set; } = null!;
}

public class LopovBasic(int id, int nivoZamki, int nivoBuke) : KlasaBasic(id) {
    public int NivoZamki { get; set; } = nivoZamki;
    public int NivoBuke { get; set; } = nivoBuke;
}

public class CarobnjakBasic(int id, string magije) : KlasaBasic(id)
{
    public string Magije { get; set; } = magije;
}

public class BoracBasic(int id, int koristiStit, int dualWilder) : KlasaBasic(id)
{
    public int KoristiStit { get; set; } = koristiStit;
    public int DualWilder { get; set; } = dualWilder;
}

public class SvestenikBasic(int id, string religija, string blagoslovi, int canHeal) : KlasaBasic(id)
{
    public string Religija { get; set; } = religija;
    public string Blagoslovi { get; set; } = blagoslovi;
    public int CanHeal { get; set; } = canHeal;
}

public class OklopnikBasic(int id, int maxOklop) : KlasaBasic(id)
{
    public int MaxOklop { get; set; } = maxOklop;
}

public class StrelacBasic(int id, int lukIliSamostrel) : KlasaBasic(id)
{
    public int LukIliSamostrel { get; set; } = lukIliSamostrel;
}
#endregion

#region Rase
public abstract class RasaBasic(int id) {
    public int Id { get; set; } = id;
    public LikBasic Lik { get; set; } = null!;
}

public class PatuljakBasic(int id, string tipOruzja) : RasaBasic(id) {
    public string TipOruzja { get; set; } = tipOruzja;
}

public class OrkBasic(int id, string tipOruzja) : RasaBasic(id) {
    public string TipOruzja { get; set; } = tipOruzja;
}

public class VilenjakBasic(int id, int nivoPotrebneMagije) : RasaBasic(id) {
    public int NivoPotrebneMagije { get; set; } = nivoPotrebneMagije;
}

public class DemonBasic(int id, int nivoPotrebneMagije) : RasaBasic(id) {
    public int NivoPotrebneMagije { get; set; } = nivoPotrebneMagije;
}

public class CovekBasic(int id, int uspesnostUSkrivanju) : RasaBasic(id) {
    public int UspesnostUSkrivanju { get; set; } = uspesnostUSkrivanju;
}
#endregion

#region SlabiEntiteti

public class SesijaPregled(DateTime vremeOd, int duzinaMin) {
    public DateTime Vreme { get; set; } = vremeOd;
    public int Duzina { get; set; } = duzinaMin;
}

public class SesijaBasic(int zlato, int xp, DateTime vreme, int duzina, int igracId) {
    public int Id { get; set; }
    public int Zlato { get; set; } = zlato;
    public int Xp { get; set; } = xp;
    public DateTime Vreme { get; set; } = vreme;
    public int Duzina { get; set; } = duzina;
    public int IgracId { get; set; } = igracId;
}

public class PomocnikPregled(int id, string ime, string rasa, string klasa, int bonusZastita) {
    public int Id { get; set; } = id;
    public string Ime { get; set; } = ime;
    public string Rasa { get; set; } = rasa;
    public string Klasa { get; set; } = klasa;
    public int BonusZastita { get; set; } = bonusZastita;
}

public class PomocnikBasic(string ime, string rasa, string klasa, int bonusZastita, int igracId) {
    public int Id { get; set; }
    public string Ime { get; set; } = ime;
    public string Rasa { get; set; } = rasa;
    public string Klasa { get; set; } = klasa;
    public int BonusZastita { get; set; } = bonusZastita;
    public int IgracId { get; set; } = igracId;
}
#endregion SlabiEntiteti

public class TimPregled(int id, string ime, int maxIgraca, int bonusXp, DateTime vremeOd, int brojClanova, int brojPobeda, int brojPoraza) {
    public int Id { get; set; } = id;
    public string Ime { get; set; } = ime;
    public int MaxIgraca { get; set; } = maxIgraca;
    public int BonusXp { get; set; } = bonusXp;
    public DateTime VremeOd { get; set; } = vremeOd;
    public int BrojClanova { get; set; } = brojClanova;
    public int BrojPobeda { get; set; } = brojPobeda;
    public int BrojPoraza { get; set; } = brojPoraza;
}

public class ClanTimaPregled(int id, string nadimak, string rasa, string klasa) {
    public int Id { get; set; } = id;
    public string Nadimak { get; set; } = nadimak;
    public string Rasa { get; set; } = rasa;
    public string Klasa { get; set; } = klasa;
} 

public class TimBasic(string ime, int maxIgraca, int bonusXp, int igracId) {
    public string Ime { get; set; } = ime;
    public int MaxIgraca { get; set; } = maxIgraca;
    public int BonusXp { get; set; } = bonusXp;
    public int IgracId { get; set; } = igracId;
}

public class StazaPregled {
    public int Id { get; set; }
    
    public string Naziv { get; set; }
    public int BonusXp { get; set; }
    public bool TimskaStaza { get; set; }
    public List<string> Rase { get; set; }
    public List<string> Klase { get; set; }
    public String RaseRepr { get; set; }
    public String KlaseRepr { get; set; }

    public StazaPregled(int id, string naziv, int bonusXp, int timskaStaza, List<string> rase, List<string> klase) {
        Id = id;
        Naziv = naziv;
        BonusXp = bonusXp;
        TimskaStaza = timskaStaza == 1;
        Rase = rase;
        Klase = klase;
        RaseRepr = string.Join("\n", Rase);
        KlaseRepr = string.Join("\n", Klase);
    }
}

#region Leaderboards
public class TeamWithMostWinsPregled(string naziv, int brojPobeda) {
    public string Naziv { get; set; } = naziv;
    public int BrojPobeda { get; set; } = brojPobeda;
}

public class TeamWithHighestWinPercentage(string naziv, float percent) {
    public string Naziv { get; set; } = naziv;
    public float Procenat { get; set; } = percent;
}

public class PlayerWithMostGold(string nadimak, int gold) {
    public string Nadimak { get; set; } = nadimak;
    public int Gold { get; set; } = gold;
}

public class PlayerWithMostXp(string nadimak, int xp) {
    public string Nadimak { get; set; } = nadimak;
    public int Xp { get; set; } = xp;
}

public class LeaderboardsPregled(
    List<TeamWithMostWinsPregled> teamsWithMostWins,
    List<TeamWithHighestWinPercentage> teamsWithHeighestPercentage,
    List<PlayerWithMostGold> playersWithMostGold,
    List<PlayerWithMostXp> playersWithMostXp) {
    public List<TeamWithMostWinsPregled> TeamsWithMostWinsPregled { get; set; } = teamsWithMostWins;
    public List<TeamWithHighestWinPercentage> TeamsWithHighestWinPercentage { get; set; } = teamsWithHeighestPercentage;
    public List<PlayerWithMostGold> PlayersWithMostGold { get; set; } = playersWithMostGold;
    public List<PlayerWithMostXp> PlayersWithMostXp { get; set; } = playersWithMostXp;
}
#endregion

public class ShoppableOrudjePregled(int id, string naziv, string opis) {
    public int Id { get; set; } = id;
    public string Naziv { get; set; } = naziv;
    public string Opis { get; set; } = opis;
}

public class OrudjePregled(int id, string naziv, string opis, string vrsta) {
    public int Id { get; set; } = id;
    public string Naziv { get; set; } = naziv;
    public string Opis { get; set; } = opis;
    public string Vrsta { get; set; } = vrsta.Split(".").Last();
}