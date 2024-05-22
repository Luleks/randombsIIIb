using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class DemonMapiranja : SubclassMap<Demon> {
    public DemonMapiranja() {
        Table("LIK");

        DiscriminatorValue("DEMON");
        Map(x => x.NivoPotrebneMagije).Column("NIVO_ENERGIJE_ZA_MAGIJU");
    }
}