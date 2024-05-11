using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class RasaMapiranja : ClassMap<Rasa> {

    public RasaMapiranja() {
        Table("RASA");

        DiscriminateSubClassesOnColumn("RASA");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        References(x => x.Lik).Column("LIK_ID");
    }
}