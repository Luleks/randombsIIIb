using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class OrudjeRestrictionKlasaMapiranja : ClassMap<OrudjeRestrictionKlasa> {
    public OrudjeRestrictionKlasaMapiranja() {
        Table("ORUDJE_RESTRICTION_KLASA");

        Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Klasa).Column("Klasa");

        References(x => x.Oruzje).Column("ORUDJE_ID").LazyLoad();
    }
}