using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class StrelacMapiranja : SubclassMap<Strelac> {
    public StrelacMapiranja() {
        Table("STRELAC");
        
        Abstract();

        Map(x => x.LukIliSamostrel).Column("LUK_ILI_SAMOSTREL");
    }
}