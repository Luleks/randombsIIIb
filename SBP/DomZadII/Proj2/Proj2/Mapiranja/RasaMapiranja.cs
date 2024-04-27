using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class RasaMapiranja : ClassMap<Rasa> {

    public RasaMapiranja() {
        Table("LIK");

        DiscriminateSubClassesOnColumn("RASA");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();
    }
}