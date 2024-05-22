using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class CarobnjakMapiranja : SubclassMap<Carobnjak> {
    public CarobnjakMapiranja() {
        Table("CAROBNJAK");

        Abstract();

        Map(x => x.Magije).Column("MAGIJE");
    }
}