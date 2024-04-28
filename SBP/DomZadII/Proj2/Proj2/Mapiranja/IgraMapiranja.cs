using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class IgraMapiranja : ClassMap<Igra> {
    public IgraMapiranja() {
        Table("IGRA");

        Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Vreme).Column("VREME");

        References(x => x.Grupa).Column("GRUPA_ID");
        References(x => x.Staza).Column("STAZA_ID");
        References(x => x.FindableOrudje).Column("FINDABLE_ORUDJE_ID");
    }
}