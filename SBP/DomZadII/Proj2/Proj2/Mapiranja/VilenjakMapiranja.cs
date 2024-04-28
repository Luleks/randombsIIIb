using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class VilenjakMapiranja : SubclassMap<Vilenjak> {
    public VilenjakMapiranja() {
        Table("RASA");

        DiscriminatorValue("VILENJAK");

        Map(x => x.NivoPotrebneMagije).Column("NIVO_ENERGIJE_ZA_MAGIJU");
    }
}