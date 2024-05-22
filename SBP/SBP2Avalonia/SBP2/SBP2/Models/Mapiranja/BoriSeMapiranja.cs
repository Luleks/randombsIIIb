using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class BoriSeMapiranja : ClassMap<BoriSe> {
    BoriSeMapiranja() {
        Table("BORI_SE");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();
        Map(x => x.Vreme).Column("VREME");
        Map(x => x.Bonus).Column("BONUS");

        References(x => x.Tim1).Column("TIM1_ID").LazyLoad();
        References(x => x.Tim2).Column("TIM2_ID").LazyLoad();
        References(x => x.Pobednik).Column("POBEDNIK_ID").LazyLoad();
    }
}