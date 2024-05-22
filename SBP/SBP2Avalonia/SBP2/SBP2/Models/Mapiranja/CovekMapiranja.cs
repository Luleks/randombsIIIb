using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class CovekMapiranja : SubclassMap<Covek> {
    public CovekMapiranja() {
        Table("LIK");
        
        DiscriminatorValue("COVEK");

        Map(x => x.Skrivanje).Column("USPESNOST_U_SKRIVANJU");
    }
}