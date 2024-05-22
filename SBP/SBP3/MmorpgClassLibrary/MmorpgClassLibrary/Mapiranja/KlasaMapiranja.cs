using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class KlasaMapiranja : ClassMap<Klasa> {
    public KlasaMapiranja() {
        UseUnionSubclassForInheritanceMapping();
        
        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        References(x => x.Lik).Column("LIK_ID");
    }
}