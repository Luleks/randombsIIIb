using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class CarobnjakMapiranja : SubclassMap<Carobnjak> {
    public CarobnjakMapiranja() {
        Table("CAROBNJAK");

        Abstract();

        Map(x => x.Magije).Column("MAGIJE");
    }
}