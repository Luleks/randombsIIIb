using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class StazaMapiranja : ClassMap<Staza> {
    public StazaMapiranja() {
        Table("STAZA");

        Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Naziv).Column("NAZIV");
        Map(x => x.BonusXp).Column("BONUS_XP");
        Map(x => x.TimskaStaza).Column("TIMSKA_STAZA");
        Map(x => x.RestrictedStaza).Column("RESTRICTED_STAZA");

        HasMany(x => x.Igranja).KeyColumn("STAZA_ID").Cascade.All().Inverse();
        HasMany(x => x.OgranicenjaKlase).KeyColumn("RESTRICTED_STAZA_ID").Cascade.All().Inverse();
        HasMany(x => x.OgranicenjaRase).KeyColumn("RESTRICTED_STAZA_ID").Cascade.All().Inverse();
    }
}