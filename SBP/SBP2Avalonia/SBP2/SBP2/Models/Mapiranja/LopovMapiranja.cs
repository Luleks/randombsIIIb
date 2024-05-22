using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class LopovMapiranja : SubclassMap<Lopov> {
    public LopovMapiranja() {
        Table("LOPOV");
        
        Abstract();

        Map(x => x.NivoBuke).Column("NIVO_BUKE");
        Map(x => x.NivoZamki).Column("NIVO_ZAMKI");
    }
}