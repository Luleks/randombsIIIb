using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class SesijaMapiranja : ClassMap<Sesija> {
    SesijaMapiranja() {
        Table("SESIJA");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();
        Map(x => x.Vreme, "VREME");
        Map(x => x.Duzina, "DUZINA");
        Map(x => x.Zlato, "ZLATO");
        Map(x => x.Xp, "XP");

        References(x => x.Igrac).Column("IGRAC_ID").LazyLoad();
    }
}