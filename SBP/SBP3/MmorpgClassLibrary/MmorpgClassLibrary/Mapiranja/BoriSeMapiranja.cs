using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class BoriSeMapiranja : ClassMap<BoriSe> {
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