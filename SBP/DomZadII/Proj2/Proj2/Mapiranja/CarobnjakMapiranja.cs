using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class CarobnjakMapiranja : SubclassMap<Carobnjak> {
    public CarobnjakMapiranja() {
        Table("CAROBNJAK");

        Abstract();

        Map(x => x.Magije).Column("MAGIJE");
    }
}