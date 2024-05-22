using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class KlasaMapiranja : ClassMap<Klasa> {
    public KlasaMapiranja() {
        UseUnionSubclassForInheritanceMapping();
        
        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        References(x => x.Lik).Column("LIK_ID");
    }
}