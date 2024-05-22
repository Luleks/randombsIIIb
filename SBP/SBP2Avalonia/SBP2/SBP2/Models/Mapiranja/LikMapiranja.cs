using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class LikMapiranja : ClassMap<Lik> {
    public LikMapiranja() {
        Table("LIK");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Iskustvo).Column("ISKUSTVO");
        Map(x => x.NivoZdravlja).Column("NIVO_ZDRAVLJA");
        Map(x => x.StepenZamora).Column("STEPEN_ZAMORA");
        Map(x => x.Zlato).Column("ZLATO");

        HasOne(x => x.Rasa).PropertyRef(x => x.Lik).Cascade.None().Not.LazyLoad();
        HasOne(x => x.Klasa).PropertyRef(x => x.Lik).Cascade.None().Not.LazyLoad();
        References(x => x.Igrac).Column("IGRAC_ID");
    }
}