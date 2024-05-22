using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class OrudjeRestrictionRasaMapiranja : ClassMap<OrudjeRestrictionRasa> {
    public OrudjeRestrictionRasaMapiranja() {
        Table("ORUDJE_RESTRICTION_RASA");

        Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Rasa).Column("RASA");

        References(x => x.Oruzje).Column("ORUDJE_ID").LazyLoad();
    }
}