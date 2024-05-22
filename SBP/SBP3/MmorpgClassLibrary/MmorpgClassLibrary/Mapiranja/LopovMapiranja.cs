using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class LopovMapiranja : SubclassMap<Lopov> {
    public LopovMapiranja() {
        Table("LOPOV");
        
        Abstract();

        Map(x => x.NivoBuke).Column("NIVO_BUKE");
        Map(x => x.NivoZamki).Column("NIVO_ZAMKI");
    }
}