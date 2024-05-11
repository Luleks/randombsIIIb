namespace Proj2;

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

public abstract class KlasaBasic(int id) {
    public int Id { get; set; } = id;
    public LikBasic Lik { get; set; } = null!;
}

public class LopovBasic(int id, int nivoZamki, int nivoBuke) : KlasaBasic(id) {
    public int NivoZamki { get; set; } = nivoZamki;
    public int NivoBuke { get; set; } = nivoBuke;
}

public abstract class RasaBasic(int id) {
    public int Id { get; set; } = id;
    public LikBasic Lik { get; set; } = null!;
}

public class PatuljakBasic(int id, string tipOruzja) : RasaBasic(id) {
    public string TipOruzja { get; set; } = tipOruzja;
}