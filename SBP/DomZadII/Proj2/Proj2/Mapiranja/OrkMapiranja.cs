using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class OrkMapiranja : SubclassMap<Ork> {
    public OrkMapiranja() {
        Table("LIK");

        DiscriminatorValue("ORK");
        Map(x => x.Oruzje).Column("TIP_ORUZJA");
    }
}