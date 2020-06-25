using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MokeponGame.Screens;
using MokeponGame.Managers;

namespace MokeponGame.Gameplay.Abilities
{
    public class Absorb : Ability
    {
        public Absorb()
        {
            Name = "Absorb";
            ACC = 90;
            DMG = 20;
            Attack = true;
            Special = true;
            AbilityType = MokeponTypes.DARK;
        }

        public override void Effect(ref Mokepon caster, ref Mokepon target, BattleScreen screen)
        {
            Damage damage = GameManager.Instance.CalculateDamage(caster, target, this);
            screen.Dialogues.AddText(caster.Name + " absorbed " + target.Name + "'s health points");
            caster.Heal((int)Math.Ceiling(damage.DamageValue / 2.0));
        }
    }
}
