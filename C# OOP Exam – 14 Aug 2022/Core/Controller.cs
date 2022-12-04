using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Repositories.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        public Controller()
        {
            planets = new PlanetRepository();
        }
        private IRepository<IPlanet> planets;
        public string AddUnit(string unitTypeName, string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            if (unitTypeName != "AnonymousImpactUnit" && unitTypeName != "SpaceForces" && unitTypeName != "StormTroopers")
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }
            if (planet.Army.Any(w => w.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));
            }

            IMilitaryUnit unit = null;
            if (unitTypeName == "AnonymousImpactUnit")
            {
                unit = new AnonymousImpactUnit();
            }
            else if (unitTypeName == "SpaceForces")
            {
                unit = new SpaceForces();

            }
            else if (unitTypeName == "StormTroopers")
            {
                unit = new StormTroopers();

            }

            planet.Spend(unit.Cost);
            planet.AddUnit(unit);

            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            if (planet.Weapons.Any(w => w.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
            }
            if (weaponTypeName != "BioChemicalWeapon" && weaponTypeName != "NuclearWeapon" && weaponTypeName != "SpaceMissiles")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }

            IWeapon weapon = null;
            if (weaponTypeName == "BioChemicalWeapon")
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }
            else if (weaponTypeName == "NuclearWeapon")
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else if (weaponTypeName == "SpaceMissiles")
            {
                weapon = new SpaceMissiles(destructionLevel);
            }
            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);

            return String.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string CreatePlanet(string name, double budget)
        {
            if (planets.FindByName(name) != null)
            {
                return String.Format(OutputMessages.ExistingPlanet, name);
            }

            IPlanet planet = new Planet(name, budget);
            planets.AddItem(planet);

            return string.Format(OutputMessages.NewPlanet, name);
        }

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");
            foreach (var planet in planets.Models.OrderByDescending(p => p.MilitaryPower).ThenBy(p => p.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet planet1 = planets.FindByName(planetOne);
            IPlanet planet2 = planets.FindByName(planetTwo);

            bool nucWepPlanOne = planet1.Weapons.Any(w => w.GetType().Name == "NuclearWeapon");
            bool nucWepPlanTwo = planet2.Weapons.Any(w => w.GetType().Name == "NuclearWeapon");
            if (planet1.MilitaryPower == planet2.MilitaryPower)
            {
                if (!nucWepPlanOne && !nucWepPlanTwo)
                {
                    return NoWinner(planet1, planet2);
                }
                if (nucWepPlanOne && nucWepPlanTwo)
                {
                    return NoWinner(planet1, planet2);

                }

                if (nucWepPlanOne)
                {
                    return Winner(planet1, planet2);
                }
                else if (nucWepPlanTwo)
                {
                    return Winner(planet2, planet1);
                }
            }
            else
            {
                if (planet1.MilitaryPower > planet2.MilitaryPower)
                {
                    return Winner(planet1, planet2);
                }
                else
                {
                    return Winner(planet2, planet1);
                }
            }
            return null;
        }

        private string Winner(IPlanet planet1, IPlanet planet2)
        {
            planet1.Spend(planet1.Budget / 2);
            double defenderHaldBudged = planet2.Budget / 2;
            planet1.Profit(defenderHaldBudged);
            double allArmyCost = planet2.Army.Sum(x => x.Cost);
            double allWeaponsCost = planet2.Weapons.Sum(x => x.Price);

            planet1.Profit(allArmyCost + allWeaponsCost);

            planets.RemoveItem(planet2.Name);

            return string.Format(OutputMessages.WinnigTheWar, planet1.Name, planet2.Name);

        }
        public string NoWinner(IPlanet planetOne, IPlanet planetTwo)
        {
            planetOne.Spend(planetOne.Budget / 2);
            planetTwo.Spend(planetTwo.Budget / 2);

            return OutputMessages.NoWinner;
        }

        public string SpecializeForces(string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            if (planet.Army.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.NoUnitsFound);
            }

            planet.Spend(1.25);
            planet.TrainArmy();

            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }
    }
}
