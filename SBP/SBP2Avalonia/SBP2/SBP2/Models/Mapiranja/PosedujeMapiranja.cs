using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class PosedujeMapiranja : ClassMap<Poseduje> {
    public PosedujeMapiranja() {
        Table("POSEDUJE");

        Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

        References(x => x.Igrac).Column("IGRAC_ID");
        References(x => x.KljucniPredmet).Column("KLJUCNI_PREDMET_ID");
    }
    
}