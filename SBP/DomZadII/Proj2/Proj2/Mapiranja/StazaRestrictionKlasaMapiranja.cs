using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class StazaRestrictionKlasaMapiranja : ClassMap<StazaRestrictionKlasa> {
    public StazaRestrictionKlasaMapiranja() {
        Table("ORUDJE_RESTRICTION_KLASA");

        Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Klasa).Column("KLASA");

        References(x => x.Staza).Column("RESTRICTED_STAZA_ID").LazyLoad();
    }
}