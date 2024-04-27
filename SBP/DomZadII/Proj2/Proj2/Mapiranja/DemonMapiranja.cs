using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class DemonMapiranja : SubclassMap<Demon> {
    public DemonMapiranja() {
        Table("LIK");

        DiscriminatorValue("DEMON");
        Map(x => x.NivoPotrebneMagije).Column("NIVO_ENERGIJE_ZA_MAGIJU");
    }
}