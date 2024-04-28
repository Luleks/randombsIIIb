using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class PosedujeMapiranja : ClassMap<Poseduje> {
    public PosedujeMapiranja() {
        Table("POSEDUJE");

        Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

        References(x => x.Igrac).Column("IGRAC_ID");
        References(x => x.KljucniPredmet).Column("KLJUCNI_PREDMET_ID");
    }
    
}