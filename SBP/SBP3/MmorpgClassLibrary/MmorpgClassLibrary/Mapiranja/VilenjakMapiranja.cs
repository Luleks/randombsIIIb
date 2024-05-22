using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class VilenjakMapiranja : SubclassMap<Vilenjak> {
    public VilenjakMapiranja() {
        Table("RASA");

        DiscriminatorValue("VILENJAK");

        Map(x => x.NivoPotrebneMagije).Column("NIVO_ENERGIJE_ZA_MAGIJU");
    }
}