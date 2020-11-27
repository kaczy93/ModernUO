using System;
using Server.Mobiles;

namespace Server.Spells.Eighth
{
    public class AirElementalSpell : MagerySpell
    {
        private static readonly SpellInfo m_Info = new(
            "Air Elemental",
            "Kal Vas Xen Hur",
            269,
            9010,
            false,
            Reagent.Bloodmoss,
            Reagent.MandrakeRoot,
            Reagent.SpidersSilk
        );

        public AirElementalSpell(Mobile caster, Item scroll = null) : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle => SpellCircle.Eighth;

        public override bool CheckCast()
        {
            if (!base.CheckCast())
            {
                return false;
            }

            if (Caster.Followers + 2 > Caster.FollowersMax)
            {
                Caster.SendLocalizedMessage(1049645); // You have too many followers to summon that creature.
                return false;
            }

            return true;
        }

        public override void OnCast()
        {
            if (CheckSequence())
            {
                var duration = TimeSpan.FromSeconds(2 * Caster.Skills.Magery.Fixed / 5.0);

                if (Core.AOS)
                {
                    SpellHelper.Summon(new SummonedAirElemental(), Caster, 0x217, duration, false, false);
                }
                else
                {
                    SpellHelper.Summon(new AirElemental(), Caster, 0x217, duration, false, false);
                }
            }

            FinishSequence();
        }
    }
}
