using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class IgracMapiranja : ClassMap<Igrac> {
    IgracMapiranja() {
        Table("IGRAC");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Nadimak).Column("NADIMAK");
        Map(x => x.Lozinka).Column("LOZINKA");
        Map(x => x.Ime).Column("IME");
        Map(x => x.Pol).Column("POL");
        Map(x => x.Prezime).Column("PREZIME");
        Map(x => x.Uzrast).Column("UZRAST");

        HasOne(x => x.Lik).PropertyRef(x => x!.Igrac).Not.LazyLoad().Cascade.None();
        HasMany(x => x.Sesije).KeyColumn("IGRAC_ID").LazyLoad().Cascade.All().Inverse();
        HasMany(x => x.Pomocnici).KeyColumn("IGRAC_ID").LazyLoad().Cascade.All().Inverse();
        HasMany(x => x.Timovi).KeyColumn("IGRAC_ID").LazyLoad().Cascade.All().Inverse();
        HasMany(x => x.Kupovine).KeyColumn("IGRAC_ID").LazyLoad().Cascade.All().Inverse();
        HasMany(x => x.KljucniPredmeti).KeyColumn("IGRAC_ID").LazyLoad().Cascade.All().Inverse();
        HasMany(x => x.Grupe).KeyColumn("IGRAC_ID").LazyLoad().Cascade.All().Inverse();
    }
}