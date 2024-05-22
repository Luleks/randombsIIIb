using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class OklopnikMapiranja : SubclassMap<Oklopnik> {
    public OklopnikMapiranja() {
        Table("OKLOPNIK");
        
        Abstract();

        Map(x => x.MaxOklop).Column("MAX_OKLOP");
    }
}