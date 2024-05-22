using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class OrkMapiranja : SubclassMap<Ork> {
    public OrkMapiranja() {
        Table("LIK");

        DiscriminatorValue("ORK");
        Map(x => x.Oruzje).Column("TIP_ORUZJA");
    }
}