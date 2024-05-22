using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

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