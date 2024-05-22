using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class GrupaMapiranja : ClassMap<Grupa> {
    public GrupaMapiranja() {
        Table("GRUPA");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Dummy).Column("DUMMY");

        HasMany(x => x.Clanovi).KeyColumn("GRUPA_ID").Cascade.All().Inverse();
        HasOne(x => x.Igra).PropertyRef(x => x.Grupa).Cascade.None();
    }
}