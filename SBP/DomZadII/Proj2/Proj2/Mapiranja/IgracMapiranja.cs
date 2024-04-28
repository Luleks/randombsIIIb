using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class IgracMapiranja : ClassMap<Igrac> {
    IgracMapiranja() {
        Table("IGRAC");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Nadimak).Column("NADIMAK");
        Map(x => x.Lozinka).Column("LOZINKA");
        Map(x => x.Ime).Column("IME");
        Map(x => x.Pol).Column("POL");
        Map(x => x.Prezime).Column("PREZIME");
        Map(x => x.Uzrast).Column("UZRAST");

        HasOne(x => x.Lik).PropertyRef(x => x.Igrac).Cascade.All();
        HasMany(x => x.Sesije).KeyColumn("IGRAC_ID").LazyLoad().Cascade.All().Inverse();
        HasMany(x => x.Pomocnici).KeyColumn("IGRAC_ID").LazyLoad().Cascade.All().Inverse();
        HasMany(x => x.Timovi).KeyColumn("IGRAC_ID").LazyLoad().Cascade.All().Inverse();
    }
}