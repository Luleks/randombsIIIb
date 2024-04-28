using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class PredmetMapiranja : SubclassMap<Predmet> {
    public PredmetMapiranja() {
        Table("ORUDJE");
        
        DiscriminatorValue("PREDMET");

        Map(x => x.DodatniXp).Column("ADDITIONAL_XP");
        Map(x => x.KljucniPredmet).Column("KLJUCNI_PREDMET");
    }
}