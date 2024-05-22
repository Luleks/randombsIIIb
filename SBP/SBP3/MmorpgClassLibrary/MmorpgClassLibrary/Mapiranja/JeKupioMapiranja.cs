using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class JeKupioMapiranja : ClassMap<JeKupio> {
    public JeKupioMapiranja() {
        Table("JE_KUPIO");

        Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

        References(x => x.Igrac).Column("IGRAC_ID");
        References(x => x.ShoppableOrudje).Column("SHOPPABLE_ORUDJE_ID");
    }
}