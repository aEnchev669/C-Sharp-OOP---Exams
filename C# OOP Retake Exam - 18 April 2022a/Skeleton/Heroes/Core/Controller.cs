using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Core
{
    public class Controller : IController
    {
        public Controller()
        {
            heroes = new HeroRepository();
            weapons = new WeaponRepository();
        }
        private IRepository<IHero> heroes;
        private IRepository<IWeapon> weapons;
        public string AddWeaponToHero(string weaponName, string heroName)
        {
            IHero hero = heroes.FindByName(heroName);
            if (hero == null)
            {
                throw new InvalidOperationException($"Hero {heroName} does not exist.");
            }

            IWeapon weapon = weapons.FindByName(weaponName);
            if (weapon == null)
            {
                throw new InvalidOperationException($"Weapon {weaponName} does not exist.");
            }
                 //!!!!!!!!!!!!!!!!!!!!!!!!
            if (hero.Weapon.GetType().Name != null)
            {
                throw new InvalidOperationException($"Hero {heroName} is well-armed.");
            }

            hero.AddWeapon(weapon);

            return $"Hero {heroName} can participate in battle using a {weaponName.ToLower()}.";

        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            IHero hero = heroes.FindByName(name);
            if (hero != null)
            {
                throw new InvalidOperationException($"The hero {name} already exists.");
            }

            if (type == "Knight")
            {
                hero = new Knight(name, health, armour);
                heroes.Add(hero);

                return $"Successfully added Sir {name} to the collection.";
            }
            else if (type == "Barbarian")
            {
                hero = new Barbarian(name, health, armour);
                heroes.Add(hero);

                return "Successfully added Barbarian { name } to the collection.";
            }
            else
            {
                throw new InvalidOperationException("Invalid hero type.");
            }


        }

        public string CreateWeapon(string type, string name, int durability)
        {
            IWeapon weapon = weapons.FindByName(name);
            if (weapon != null)
            {
                throw new InvalidOperationException($"The weapon {name} already exists.");
            }

            if (type == "Mace")
            {
                weapon = new Mace(name, durability);
            }
            else if (type == "Claymore")
            {
                weapon = new Claymore(name, durability);
            }
            else
            {
                throw new InvalidOperationException("Invalid weapon type.");
            }

            weapons.Add(weapon);
            return $"A {type.ToLower()} {name} is added to the collection.";
        }

        public string HeroReport()
        {
            throw new NotImplementedException();
        }

        public string StartBattle()
        {
            throw new NotImplementedException();
        }
    }
}
