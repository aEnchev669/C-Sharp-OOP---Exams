using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Inventory;

namespace WarCroft.Entities.Characters
{
    public class Priest : Character ,IHealer
    {
        private static Bag bag;

        public Priest(string name) : base(name, 50, 25, 40, bag)
        {
           bag = new Backpack();
        }

        public void Heal(Character character)
        {
            if (character.IsAlive && this.IsAlive)
            {
                character.Health+= 20;
            }
        }
    }
}
