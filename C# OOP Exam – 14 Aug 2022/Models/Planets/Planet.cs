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
        private IRepository<IMilitaryUnit> units;
        private IRepository<IWeapon> weapons;
        private string name;

        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;

            units = new UnitRepository();
            weapons = new WeaponRepository();

        }
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

        public double MilitaryPower => CalculateTotalAmount();                                                                                                                                                                                                                                                           
        




        public IReadOnlyCollection<IMilitaryUnit> Army => units.Models;

        public IReadOnlyCollection<IWeapon> Weapons => weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
        {
           units.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            weapons.AddItem(weapon);
        }

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();

            string forces = units.Models.Any() ? string.Join(", ", units.Models.Select(a => a.GetType().Name)) : "No units";       //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            sb.AppendLine($"Planet: {Name}")
                .AppendLine($"--Budget: {Budget} billion QUID")
                .AppendLine($"--Forces: {forces}")
                .AppendLine($"--Combat equipment: {(weapons.Models.Any() ? string.Join(", ", weapons.Models.Select(w => w.GetType().Name)) : "No weapons")}")      //!!!!!!!!!!!!!!
                .AppendLine($"--Military Power: {MilitaryPower}");

            return sb.ToString().TrimEnd();
        }

        public void Profit(double amount)
        {
            budget += amount;
        }

        public void Spend(double amount)
        {
            if (budget < amount)                                          //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);
            }
            budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var item in Army)
            {
                item.IncreaseEndurance();
            }
        }

        private double CalculateTotalAmount()
        {
            double totalAmount = units.Models.Sum(u => u.EnduranceLevel) + weapons.Models.Sum(w => w.DestructionLevel);

            if (units.Models.Any(u => u.GetType().Name == "AnonymousImpactUnit"))
            {
                totalAmount *= 1.30;
            }
            if (weapons.Models.Any(w => w.GetType().Name == "NuclearWeapon"))
            {
                totalAmount *= 1.45;
            }
            return Math.Round(totalAmount, 3);
        }
    }
}
