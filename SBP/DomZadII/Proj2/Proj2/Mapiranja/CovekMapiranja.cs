using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class CovekMapiranja : SubclassMap<Covek> {
    public CovekMapiranja() {
        Table("LIK");
        
        DiscriminatorValue("COVEK");

        Map(x => x.Skrivanje).Column("USPESNOST_U_SKRIVANJU");
    }
}