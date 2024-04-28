using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class OruzjeMapiranja : SubclassMap<Oruzje> {
    public OruzjeMapiranja() {
        Table("ORUDJE");
        
        DiscriminatorValue("ORUZJE");

        Map(x => x.DodatniXp).Column("ADDITIONAL_XP");
        Map(x => x.PoeniZaNapad).Column("ATK_DEF_POENI");
    }
}