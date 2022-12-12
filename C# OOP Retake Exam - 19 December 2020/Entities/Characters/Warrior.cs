using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Inventory;

namespace WarCroft.Entities.Characters
{
    public class Warrior : Character ,IAttacker
    {
        private static Satchel bag;
        public Warrior(string name) : base(name, 100, 50, 40, bag)
        {
            bag = new Satchel();
        }

        public void Attack(Character character)
        {
            if (character.Name == this.Name)
            {
                throw new InvalidOperationException("Cannot attack self!");
            }

            if (character.IsAlive && this.IsAlive)
            {
                character.TakeDamage(this.AbilityPoints);
            }
        }
    }
}
