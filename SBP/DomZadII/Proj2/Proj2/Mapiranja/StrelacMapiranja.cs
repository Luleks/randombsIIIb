using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class StrelacMapiranja : SubclassMap<Strelac> {
    StrelacMapiranja() {
        Table("STRELAC");
        
        Abstract();

        Map(x => x.LukIliSamostrel).Column("LUK_ILI_SAMOSTREL");
    }
}