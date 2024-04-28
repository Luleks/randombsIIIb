namespace Proj2.Entiteti;

public class Tim {
    public virtual int Id { get; protected set; }
    public virtual required string Naziv { get; set; }
    public virtual int Plasman { get; set; }
    public virtual int MinIgraca { get; set; }
    public virtual int MaxIgraca { get; set; }
    public virtual int BonusXp { get; set; }
    public virtual IList<TeamMembership> Clanovi { get; set; } = [];
    public virtual IList<BoriSe> HomeBorbe { get; set; } = [];
    public virtual IList<BoriSe> GuestBorbe { get; set; } = [];
    public virtual IList<BoriSe> Pobede { get; set; } = [];
}