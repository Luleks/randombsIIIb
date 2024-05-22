using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class DemonMapiranja : SubclassMap<Demon> {
    public DemonMapiranja() {
        Table("LIK");

        DiscriminatorValue("DEMON");
        Map(x => x.NivoPotrebneMagije).Column("NIVO_ENERGIJE_ZA_MAGIJU");
    }
}