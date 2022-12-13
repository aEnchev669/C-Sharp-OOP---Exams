using System;

using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
        // TODO: Implement the rest of the class.
        public Character(string name, double health, double armor, double abilityPoints, Bag bag)
        {
            Name = name;
            BaseHealth = health;
            BaseArmor = armor;
            AbilityPoints = abilityPoints;
            Health = health;
            Armor = armor;
            this.Bag = bag;
        }
        private string name;

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }
                name = value;
            }
        }

        private double baseHealth;

        public double BaseHealth
        {
            get { return baseHealth; }
            private set                                   //only getter and setter, without field
            {
                baseHealth = value;
            }
        }

        private double health;

        public double Health
        {
            get { return health; }
            set
            {
                if (value > BaseHealth)
                {
                    value = BaseHealth;
                }
                else if (value < 0)
                {
                    value = 0;
                }
                health = value;
            }
        }

        private double baseArmor;

        public double BaseArmor
        {
            get { return baseArmor; }                                       //only getter and setter, without field
            private set
            {
                baseArmor = value;
            }
        }

        private double armor;

        public double Armor
        {
            get { return armor; }
            private set
            {
                if (value > BaseArmor)
                {
                    value = BaseArmor;
                }
                else if (value < 0)
                {
                    value = 0;
                }
                armor = value;
            }
        }

        public double AbilityPoints { get; set; }                                     //field?

        public Bag Bag { get; set; }
        public bool IsAlive { get; set; } = true;

        public void EnsureAlive()
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
        }
        public void TakeDamage(double hitPoints)
        {
            if (Armor - hitPoints < 0)
            {
                hitPoints -= Armor;
                Armor = 0;

                Health -= hitPoints;

                if (Health <= 0)
                {
                    IsAlive = false;
                    Health = 0;
                }
            }
            else
            {
           Armor -= hitPoints;
            }
        }
        public void UseItem(Item item)
        {
            this.EnsureAlive();
            item.AffectCharacter(this);
        }
    }
}