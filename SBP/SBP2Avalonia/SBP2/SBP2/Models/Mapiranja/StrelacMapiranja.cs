using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class StrelacMapiranja : SubclassMap<Strelac> {
    public StrelacMapiranja() {
        Table("STRELAC");
        
        Abstract();

        Map(x => x.LukIliSamostrel).Column("LUK_ILI_SAMOSTREL");
    }
}