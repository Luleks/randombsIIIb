using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class GroupMembershipMapiranja: ClassMap<GroupMembership> {
    public GroupMembershipMapiranja() {
        Table("GROUP_MEMBERSHIP");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        Map(x => x.PobedjeniNeprijatelji).Column("POBEDJENI_NEPRIJATELJI");
        
        References(x => x.Igrac).Column("IGRAC_ID");
        References(x => x.Grupa).Column("GRUPA_ID");
    }
}