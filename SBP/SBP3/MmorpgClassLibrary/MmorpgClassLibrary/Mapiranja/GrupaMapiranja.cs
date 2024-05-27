using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class GrupaMapiranja : ClassMap<Grupa> {
    public GrupaMapiranja() {
        Table("GRUPA");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Dummy).Column("DUMMY");

        HasMany(x => x.Clanovi).KeyColumn("GRUPA_ID").Cascade.All().Inverse();
        HasOne(x => x.Igra).PropertyRef(x => x!.Grupa).Cascade.None();
    }
}