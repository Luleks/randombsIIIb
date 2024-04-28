using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class LikMapiranja : ClassMap<Lik> {
    public LikMapiranja() {
        Table("LIK");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Iskustvo).Column("ISKUSTVO");
        Map(x => x.NivoZdravlja).Column("NIVO_ZDRAVLJA");
        Map(x => x.StepenZamora).Column("STEPEN_ZAMORA");
        Map(x => x.Zlato).Column("ZLATO");

        HasOne(x => x.Rasa).PropertyRef(x => x.Id).Cascade.All().Not.LazyLoad();
        HasOne(x => x.Klasa).PropertyRef(x => x.Id).Cascade.All().Not.LazyLoad();
        References(x => x.Igrac).Column("IGRAC_ID").Unique();
    }
}