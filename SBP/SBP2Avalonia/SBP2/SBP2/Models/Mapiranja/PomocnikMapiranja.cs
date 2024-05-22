using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class PomocnikMapiranja : ClassMap<Pomocnik> {
    PomocnikMapiranja() {
        Table("POMOCNIK");

        Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Klasa).Column("KLASA");
        Map(x => x.Rasa).Column("RASA");
        Map(x => x.BonusZastita).Column("BONUS_ZASTITA");
        Map(x => x.Ime).Column("IME");

        References(x => x.Igrac).Column("IGRAC_ID").LazyLoad();
    }
}