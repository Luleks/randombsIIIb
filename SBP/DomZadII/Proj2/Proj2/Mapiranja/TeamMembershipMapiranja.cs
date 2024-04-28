using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class TeamMembershipMapiranja : ClassMap<TeamMembership> {
    public TeamMembershipMapiranja() {
        Table("TEAM_MEMBERSHIP");

        Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();
        Map(x => x.VremeOd).Column("VREME_OD");

        References(x => x.Igrac).Column("IGRAC_ID").LazyLoad();
        References(x => x.Tim).Column("TIM_ID").LazyLoad();
    }
}