using Heroes.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models.Heroes
{
    public abstract class Hero : IHero
    {
        public Hero(string name, int health, int armour)
        {
            Name = name;
            Health = health;
            Armour = armour;
        }
        private string name;

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Hero name cannot be null or empty.");
                }
                name = value;
            }
        }


        private int health;

        public int Health
        {
            get { return health; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Hero health cannot be below 0.");
                }
                health = value;
            }
        }

        private int armour;

        public int Armour
        {
            get { return armour; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Hero armour cannot be below 0.");
                }
                armour = value;
            }
        }


        private IWeapon weapon;

        public IWeapon Weapon
        {
            get { return weapon; }
             private set
            {
                if (value == null)
                {
                    throw new ArgumentException("Weapon cannot be null.");
                }
                weapon = value;
            }
        }



        public bool IsAlive
        {
            get
            {
                if (Health < 0)
                {
                    return false;
                }
                return true;
            }
        }

        public void AddWeapon(IWeapon weapon)
        {
            Weapon = weapon;
        }

        public void TakeDamage(int points)
        {
            Armour -= points;

            if (armour < 0)
            {
                int leftPoints = Health - Math.Abs(Armour);
                Health -= leftPoints;
                Armour = 0;
                if (Health < 0)
                {
                    Health = 0;
                }
            }
        }
    }
}
