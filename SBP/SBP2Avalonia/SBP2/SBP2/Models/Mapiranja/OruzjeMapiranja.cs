using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class OruzjeMapiranja : SubclassMap<Oruzje> {
    public OruzjeMapiranja() {
        Table("ORUDJE");
        
        DiscriminatorValue("ORUZJE");

        Map(x => x.DodatniXp).Column("ADDITIONAL_XP");
        Map(x => x.PoeniZaNapad).Column("ATK_DEF_POENI");
        
        HasMany(x => x.Kupovine).KeyColumn("SHOPPABLE_ORUDJE_ID").Cascade.All().Inverse();
        HasMany(x => x.NadjenoUIgrni).KeyColumn("FINDABLE_ORUDJE_ID").Cascade.All().Inverse();
    }
}