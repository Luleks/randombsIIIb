using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class KlasaMapiranja : ClassMap<Klasa> {
    public KlasaMapiranja() {
        UseUnionSubclassForInheritanceMapping();
        
        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        References(x => x.Lik, "LIK_ID");
    }
}