using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class GroupMembershipMapiranja: ClassMap<GroupMembership> {
    public GroupMembershipMapiranja() {
        Table("GROUP_MEMBERSHIP");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        Map(x => x.PobedjeniNeprijatelji).Column("POBEDJENI_NEPRIJATELJI");
        
        References(x => x.Igrac).Column("IGRAC_ID");
        References(x => x.Grupa).Column("GRUPA_ID");
    }
}