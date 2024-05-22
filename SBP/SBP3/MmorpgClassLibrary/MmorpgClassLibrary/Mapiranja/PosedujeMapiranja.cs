using FluentNHibernate.Mapping;
using MmorpgClassLibrary.Entiteti;

namespace MmorpgClassLibrary.Mapiranja;

internal class PosedujeMapiranja : ClassMap<Poseduje> {
    public PosedujeMapiranja() {
        Table("POSEDUJE");

        Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

        References(x => x.Igrac).Column("IGRAC_ID");
        References(x => x.KljucniPredmet).Column("KLJUCNI_PREDMET_ID");
    }
    
}