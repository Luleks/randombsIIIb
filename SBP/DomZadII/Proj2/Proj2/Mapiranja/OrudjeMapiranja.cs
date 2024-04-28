using FluentNHibernate.Mapping;
using FluentNHibernate.Utils;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

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