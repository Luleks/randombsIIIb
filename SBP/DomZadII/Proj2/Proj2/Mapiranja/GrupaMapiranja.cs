using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class GrupaMapiranja : ClassMap<Grupa> {
    public GrupaMapiranja() {
        Table("GRUPA");

        Id(x => x.Id, "ID");

        HasMany(x => x.Clanovi).KeyColumn("GRUPA_ID").Cascade.All().Inverse();
        HasOne(x => x.Igra).PropertyRef(x => x.Grupa);
    }
}