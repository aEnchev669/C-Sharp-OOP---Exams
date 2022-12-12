using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Entities.Characters.Contracts;

namespace WarCroft.Entities.Items
{
    public class HealthPotion : Item
    {
        public HealthPotion() : base(5)
        {
        }

        public override void AffectCharacter(IAttacker character)
        {
            if (character.IsAlive)
            {
                character.Health += 20;
                if (character.Health > 100)                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                {
                    character.Health = 100;
                }
            }
        }
    }
}
