using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class StazaRestrictionRasaMapiranja : ClassMap<StazaRestrictionRasa> {
    public StazaRestrictionRasaMapiranja() {
        Table("STAZA_RESTRICTION_RASA");

        Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Rasa).Column("RASA");

        References(x => x.Staza).Column("RESTRICTED_STAZA_ID").LazyLoad();
    }
}