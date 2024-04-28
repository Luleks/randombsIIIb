using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class SvestenikMapiranja : SubclassMap<Svestenik> {
    public SvestenikMapiranja() {
        Table("SVESTENIK");
        
        Abstract();

        Map(x => x.Blagoslovi).Column("BLAGOSLOVI");
        Map(x => x.Religija).Column("RELIGIJA");
        Map(x => x.CanHeal).Column("CAN_HEAL");
    }
}