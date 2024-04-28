using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class OklopMapiranja : SubclassMap<Oklop> {
    public OklopMapiranja() {
        Table("ORUDJE");
        
        DiscriminatorValue("OKLOP");

        Map(x => x.PoeniZaOdbranu).Column("ATK_DEF_POENI");

        HasMany(x => x.Kupovine).KeyColumn("SHOPPABLE_ORUDJE_ID").Cascade.All().Inverse();
    }
}