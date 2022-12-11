using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
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
            heroRepository = new HeroRepository();
            weaponRepository = new WeaponRepository();
        }
        private HeroRepository heroRepository;
        private WeaponRepository weaponRepository;
        public string AddWeaponToHero(string weaponName, string heroName)
        {
            IHero hero = heroRepository.FindByName(heroName);
            IWeapon weapon = weaponRepository.FindByName(weaponName);

            if (hero == null)
            {
                throw new InvalidOperationException($"Hero {heroName} does not exist.");
            }
            if (weapon == null)
            {
                throw new InvalidOperationException($"Weapon {weaponName} does not exist.");
            }

            if (hero.Weapon != null)
            {
                throw new InvalidOperationException($"Hero {heroName} is well-armed.");
            }

            hero.AddWeapon(weapon);

            return $"Hero {heroName} can participate in battle using a {(weapon.GetType().Name).ToLower()}.";
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            bool isKnight = false;
            IHero hero = heroRepository.FindByName(name);
            if (hero != null)
            {
                throw new InvalidOperationException($"The hero {name} already exists.");
            }
            if (type == "Barbarian")
            {
                hero = new Barbarian(name, health, armour);
            }
            else if (type == "Knight")
            {
                isKnight = true;
                hero = new Knight(name, health, armour);
            }
            else
            {
                throw new InvalidOperationException($"Invalid hero type.");
            }

            heroRepository.Add(hero);
            if (isKnight)
            {
                return $"Successfully added Sir {name} to the collection.";
            }
            return $"Successfully added Barbarian {name} to the collection.";

        }

        public string CreateWeapon(string type, string name, int durability)
        {
            IWeapon weapon = weaponRepository.FindByName(type);
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
            string typeInLowerCases = type.ToLower();

            weaponRepository.Add(weapon);
            return $"A {typeInLowerCases} {name} is added to the collection.";
        }

        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var hero in heroRepository.Models.OrderBy(h => h.GetType().Name).ThenByDescending(h => h.Health).ThenBy(h => h.Name))
            {
                sb.AppendLine($"{hero.GetType()}: {hero.Name}")
                    .AppendLine($"--Health: {hero.Health}")
                    .AppendLine($"--Armour: {hero.Armour }").
                    AppendLine($"--Weapon: {(hero.Weapon != null ? hero.Weapon.Name : "Unarmed") }");
            }
            return sb.ToString().TrimEnd();
        }

        public string StartBattle()
        {
            IMap map = new Map();

            List<IHero> heroes = new List<IHero>();
            foreach (var hero in heroRepository.Models)
            {
                if (hero.Weapon != null)
                {
                    heroes.Add(hero);
                }
            }

            return map.Fight(heroes);
        }
    }
}
