using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class OrudjeMapiranja : ClassMap<Orudje> {
    public OrudjeMapiranja() {
        Table("ORUDJE");

        DiscriminateSubClassesOnColumn("TIP_ORUDJA");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Naziv).Column("NAZIV");
        Map(x => x.Opis).Column("OPIS");

        HasMany(x => x.OgranicenjaRase).KeyColumn("ORUDJE_ID").Cascade.All().Inverse();
        HasMany(x => x.OgranicenjaKlase).KeyColumn("ORUDJE_ID").Cascade.All().Inverse();
    }
}