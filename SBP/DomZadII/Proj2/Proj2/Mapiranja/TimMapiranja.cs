using FluentNHibernate.Mapping;
using Proj2.Entiteti;

namespace Proj2.Mapiranja;

public class TimMapiranja : ClassMap<Tim> {
    public TimMapiranja() {
        Table("TIM");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Naziv).Column("NAZIV");
        Map(x => x.Plasman).Column("PLASMAN");
        Map(x => x.MaxIgraca).Column("MAX_IGRACA");
        Map(x => x.MinIgraca).Column("MIN_IGRACA");
        Map(x => x.BonusXp).Column("BONUS_XP");

        HasMany(x => x.Clanovi).KeyColumn("TIM_ID").Cascade.All().Inverse();
        HasMany(x => x.HomeBorbe).KeyColumn("TIM1").Cascade.All().Inverse();
        HasMany(x => x.GuestBorbe).KeyColumn("TIM2").Cascade.All().Inverse();
        HasMany(x => x.Pobede).KeyColumn("POBEDNIK").Cascade.All().Inverse();
    }
}