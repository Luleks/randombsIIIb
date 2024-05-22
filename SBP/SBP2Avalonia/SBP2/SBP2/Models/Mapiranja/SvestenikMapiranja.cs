using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class SvestenikMapiranja : SubclassMap<Svestenik> {
    public SvestenikMapiranja() {
        Table("SVESTENIK");
        
        Abstract();

        Map(x => x.Blagoslovi).Column("BLAGOSLOVI");
        Map(x => x.Religija).Column("RELIGIJA");
        Map(x => x.CanHeal).Column("CAN_HEAL");
    }
}