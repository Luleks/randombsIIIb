using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class OrkMapiranja : SubclassMap<Ork> {
    public OrkMapiranja() {
        Table("LIK");

        DiscriminatorValue("ORK");
        Map(x => x.Oruzje).Column("TIP_ORUZJA");
    }
}