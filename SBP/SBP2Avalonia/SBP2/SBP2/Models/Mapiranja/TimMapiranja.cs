using FluentNHibernate.Mapping;
using SBP2.Models.Entiteti;

namespace SBP2.Models.Mapiranja;

public class TimMapiranja : ClassMap<Tim> {
    public TimMapiranja() {
        Table("TIM");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Naziv).Column("NAZIV");
        Map(x => x.MaxIgraca).Column("MAX_IGRACA");
        Map(x => x.MinIgraca).Column("MIN_IGRACA");
        Map(x => x.BonusXp).Column("BONUS_XP");

        HasMany(x => x.Clanovi).KeyColumn("TIM_ID").Cascade.All().Inverse();
        HasMany(x => x.HomeBorbe).KeyColumn("TIM1_ID").Cascade.All().Inverse();
        HasMany(x => x.GuestBorbe).KeyColumn("TIM2_ID").Cascade.All().Inverse();
        HasMany(x => x.Pobede).KeyColumn("POBEDNIK_ID").Cascade.All().Inverse();
    }
}