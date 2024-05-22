using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class CovekMapiranja : SubclassMap<Covek> {
    public CovekMapiranja() {
        Table("LIK");
        
        DiscriminatorValue("COVEK");

        Map(x => x.Skrivanje).Column("USPESNOST_U_SKRIVANJU");
    }
}