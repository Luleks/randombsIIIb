using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class PatuljakMapiranja : SubclassMap<Patuljak> {
    public PatuljakMapiranja() {
        Table("LIK");
        
        DiscriminatorValue("PATULJAK");
        Map(x => x.Oruzje).Column("TIP_ORUZJA");
    }
}