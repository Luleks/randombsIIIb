using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class RasaMapiranja : ClassMap<Rasa> {

    public RasaMapiranja() {
        Table("RASA");

        DiscriminateSubClassesOnColumn("RASA");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        References(x => x.Lik).Column("LIK_ID");
    }
}