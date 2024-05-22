using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class StazaRestrictionRasaMapiranja : ClassMap<StazaRestrictionRasa> {
    public StazaRestrictionRasaMapiranja() {
        Table("STAZA_RESTRICTION_RASA");

        Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Rasa).Column("RASA");

        References(x => x.Staza).Column("RESTRICTED_STAZA_ID").LazyLoad();
    }
}