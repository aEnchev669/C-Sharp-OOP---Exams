using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Inventory;

namespace WarCroft.Entities.Characters
{
    public class Warrior : Character, IAttacker
    {
        
        public Warrior(string name) : base(name, 100, 50, 40, new Satchel())
        {
            
        }

        public void Attack(Character character)
        {
            if (this == character)
            {
                throw new InvalidOperationException("Cannot attack self!");
            }
            this.EnsureAlive();
            character.EnsureAlive();
            character.TakeDamage(this.AbilityPoints);

        }
    }
}
