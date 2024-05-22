using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class SvestenikMapiranja : SubclassMap<Svestenik> {
    public SvestenikMapiranja() {
        Table("SVESTENIK");
        
        Abstract();

        Map(x => x.Blagoslovi).Column("BLAGOSLOVI");
        Map(x => x.Religija).Column("RELIGIJA");
        Map(x => x.CanHeal).Column("CAN_HEAL");
    }
}