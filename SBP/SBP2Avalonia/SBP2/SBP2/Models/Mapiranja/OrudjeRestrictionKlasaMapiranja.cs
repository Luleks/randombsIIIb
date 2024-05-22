using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class OrudjeRestrictionKlasaMapiranja : ClassMap<OrudjeRestrictionKlasa> {
    public OrudjeRestrictionKlasaMapiranja() {
        Table("ORUDJE_RESTRICTION_KLASA");

        Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Klasa).Column("Klasa");

        References(x => x.Oruzje).Column("ORUDJE_ID").LazyLoad();
    }
}