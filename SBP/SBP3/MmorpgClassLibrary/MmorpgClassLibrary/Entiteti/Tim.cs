namespace MmorpgClassLibrary.Entiteti;

internal class Tim {
    protected internal virtual int Id { get; set; }
    protected internal virtual required string Naziv { get; set; }
    protected internal virtual int MinIgraca { get; set; }
    protected internal virtual int MaxIgraca { get; set; }
    protected internal virtual int BonusXp { get; set; }
    protected internal virtual IList<TeamMembership> Clanovi { get; set; } = [];
    protected internal virtual IList<BoriSe> HomeBorbe { get; set; } = [];
    protected internal virtual IList<BoriSe> GuestBorbe { get; set; } = [];
    protected internal virtual IList<BoriSe> Pobede { get; set; } = [];
}