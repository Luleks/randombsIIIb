using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class BoracMapiranja : SubclassMap<Borac> {
    public BoracMapiranja() {
        Table("BORAC");
        
        Abstract();

        Map(x => x.KoristiStit).Column("KORISTI_STIT");
        Map(x => x.DualWielder).Column("DUAL_WIELDER");
    }
}