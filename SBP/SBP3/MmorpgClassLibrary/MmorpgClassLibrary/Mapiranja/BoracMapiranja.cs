using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class BoracMapiranja : SubclassMap<Borac> {
    public BoracMapiranja() {
        Table("BORAC");
        
        Abstract();

        Map(x => x.KoristiStit).Column("KORISTI_STIT");
        Map(x => x.DualWielder).Column("DUAL_WIELDER");
    }
}