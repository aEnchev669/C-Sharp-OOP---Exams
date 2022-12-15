using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Repositories.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;

            army = new UnitRepository();
            weapons = new WeaponRepository();
        }
        private string name;

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);
                }
                name = value;
            }
        }

        private double budget;

        public double Budget
        {
            get { return budget; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);
                }
                budget = value;
            }
        }




        public double MilitaryPower => militaryPowerCalculator();


        public double militaryPowerCalculator()
        {
           
            double totalSum = army.Models.Sum(u => u.EnduranceLevel) + weapons.Models.Sum(w => w.DestructionLevel);

            if (army.Models.Any(m => m.GetType().Name == "AnonymousImpactUnit"))
            {
                totalSum *= 1.30;
            }
            if (weapons.Models.Any(m => m.GetType().Name == "NuclearWeapon"))
            {
                totalSum *= 1.45;
            }

            return Math.Round(totalSum, 3);
        }

        private IRepository<IMilitaryUnit> army;
        public IReadOnlyCollection<IMilitaryUnit> Army => army.Models;

        private IRepository<IWeapon> weapons;
        public IReadOnlyCollection<IWeapon> Weapons => weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
        {
            army.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            weapons.AddItem(weapon);
        }

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();

            List<string> armyNames = new List<string>();
            foreach (var item in army.Models)
            {
                armyNames.Add(item.GetType().Name);
            }
            List<string> weaponsNamesList = new List<string>();
            foreach (var item in weapons.Models)
            {
                weaponsNamesList.Add(item.GetType().Name);
            }
            string unitsInArmy = army.Models.Any() ? string.Join(", ", armyNames) : "No units";
            string weaponsNames = weapons.Models.Any() ? string.Join(", ", weaponsNamesList) : "No weapons";

            sb.AppendLine($"Planet: {this.Name}")
                .AppendLine($"--Budget: {Budget} billion QUID")
                .AppendLine($"--Forces: {unitsInArmy}")
                .AppendLine($"--Combat equipment: {weaponsNames}")
                .AppendLine($"--Military Power: {MilitaryPower}");

            return sb.ToString().TrimEnd();
        }
        public void Profit(double amount)
        {
            Budget += amount;
        }

        public void Spend(double amount)
        {
            if (Budget < amount)
            {
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);
            }
            Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var item in army.Models)
            {
                item.IncreaseEndurance();
            }
        }
    }
}
