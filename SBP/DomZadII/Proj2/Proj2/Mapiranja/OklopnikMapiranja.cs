using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class OklopnikMapiranja : SubclassMap<Oklopnik> {
    public OklopnikMapiranja() {
        Table("OKLOPNIK");
        
        Abstract();

        Map(x => x.MaxOklop).Column("MAX_OKLOP");
    }
}