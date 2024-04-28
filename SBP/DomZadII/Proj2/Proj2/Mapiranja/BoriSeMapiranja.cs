using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class BoriSeMapiranja : ClassMap<BoriSe> {
    BoriSeMapiranja() {
        Table("BORI_SE");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();
        Map(x => x.Vreme).Column("VREME");
        Map(x => x.Bonus).Column("BONUS");

        References(x => x.Tim1).Column("TIM1").LazyLoad();
        References(x => x.Tim2).Column("TIM2").LazyLoad();
        References(x => x.Pobednik).Column("POBEDNIK").LazyLoad();
    }
}