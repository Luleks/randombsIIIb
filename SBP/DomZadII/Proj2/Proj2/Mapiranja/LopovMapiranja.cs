using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class LopovMapiranja : SubclassMap<Lopov> {
    public LopovMapiranja() {
        Table("LOPOV");
        
        Abstract();

        Map(x => x.NivoBuke).Column("NIVO_BUKE");
        Map(x => x.NivoZamki).Column("NIVO_ZAMKI");
    }
}