using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class StazaRestrictionKlasaMapiranja : ClassMap<StazaRestrictionKlasa> {
    public StazaRestrictionKlasaMapiranja() {
        Table("STAZA_RESTRICTION_KLASA");

        Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Klasa).Column("KLASA");

        References(x => x.Staza).Column("RESTRICTED_STAZA_ID").LazyLoad();
    }
}