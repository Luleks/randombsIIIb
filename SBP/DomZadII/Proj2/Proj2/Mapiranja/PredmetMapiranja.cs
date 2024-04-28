using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class PredmetMapiranja : SubclassMap<Predmet> {
    public PredmetMapiranja() {
        Table("ORUDJE");
        
        DiscriminatorValue("PREDMET");

        Map(x => x.DodatniXp).Column("ADDITIONAL_XP");
        Map(x => x.KljucniPredmet).Column("KLJUCNI_PREDMET");

        HasMany(x => x.Vlasnici).KeyColumn("KLJUCNI_PREDMET_ID").LazyLoad().Cascade.All().Inverse();
        HasMany(x => x.NadjenoUIgri).KeyColumn("FINDABLE_ORUDJE_ID").Cascade.All().Inverse();
    }
}