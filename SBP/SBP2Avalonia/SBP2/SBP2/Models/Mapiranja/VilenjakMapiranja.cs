using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class VilenjakMapiranja : SubclassMap<Vilenjak> {
    public VilenjakMapiranja() {
        Table("RASA");

        DiscriminatorValue("VILENJAK");

        Map(x => x.NivoPotrebneMagije).Column("NIVO_ENERGIJE_ZA_MAGIJU");
    }
}