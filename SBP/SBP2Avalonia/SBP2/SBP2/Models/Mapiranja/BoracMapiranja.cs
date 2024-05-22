using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class BoracMapiranja : SubclassMap<Borac> {
    public BoracMapiranja() {
        Table("BORAC");
        
        Abstract();

        Map(x => x.KoristiStit).Column("KORISTI_STIT");
        Map(x => x.DualWielder).Column("DUAL_WIELDER");
    }
}