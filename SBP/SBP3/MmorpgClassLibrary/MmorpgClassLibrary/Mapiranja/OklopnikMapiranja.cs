using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class OklopnikMapiranja : SubclassMap<Oklopnik> {
    public OklopnikMapiranja() {
        Table("OKLOPNIK");
        
        Abstract();

        Map(x => x.MaxOklop).Column("MAX_OKLOP");
    }
}