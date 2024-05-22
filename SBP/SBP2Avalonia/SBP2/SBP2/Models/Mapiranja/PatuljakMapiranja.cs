using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class PatuljakMapiranja : SubclassMap<Patuljak> {
    public PatuljakMapiranja() {
        Table("LIK");
        
        DiscriminatorValue("PATULJAK");
        Map(x => x.Oruzje).Column("TIP_ORUZJA");
    }
}