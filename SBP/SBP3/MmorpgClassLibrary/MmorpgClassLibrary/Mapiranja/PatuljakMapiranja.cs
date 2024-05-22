using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class PatuljakMapiranja : SubclassMap<Patuljak> {
    public PatuljakMapiranja() {
        Table("LIK");
        
        DiscriminatorValue("PATULJAK");
        Map(x => x.Oruzje).Column("TIP_ORUZJA");
    }
}