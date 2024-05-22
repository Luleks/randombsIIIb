using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class TeamMembershipMapiranja : ClassMap<TeamMembership> {
    public TeamMembershipMapiranja() {
        Table("TEAM_MEMBERSHIP");

        Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();
        Map(x => x.VremeOd).Column("VREME_OD");
        Map(x => x.VremeDo).Column("VREME_DO");

        References(x => x.Igrac).Column("IGRAC_ID").LazyLoad();
        References(x => x.Tim).Column("TIM_ID").LazyLoad();
    }
}