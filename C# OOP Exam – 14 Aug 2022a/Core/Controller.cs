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

            IMilitaryUnit unit = null;
            if (unitTypeName == "SpaceForces")
            {
                unit = new SpaceForces();
            }
            else if (unitTypeName == "AnonymousImpactUnit")
            {
                unit = new AnonymousImpactUnit();         //Swap!!!!!!!!!!!!!!!!
            }
            else if (unitTypeName == "StormTroopers")
            {
                unit = new StormTroopers();
            }
            else
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }

            if (planet.Army.Any(a => a.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));         //Swap !!!!!!!!!!!!!!!
            }

            planet.Spend(unit.Cost);
            planet.AddUnit(unit);

            return String.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planet.Weapons.Any(w => w.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
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
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }

            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);

            return String.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string CreatePlanet(string name, double budget)
        {
            IPlanet planet = planets.FindByName(name);
            if (planet != null)
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }

            planet = new Planet(name, budget);
            planets.AddItem(planet);
            return String.Format(OutputMessages.NewPlanet, name);
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

            int winner = 0;
            if (planet1.MilitaryPower == planet2.MilitaryPower)
            {
                if (planet1.Weapons.Any(w => w.GetType().Name == "NuclearWeapon") && planet2.Weapons.Any(w => w.GetType().Name == "NuclearWeapon"))
                {

                }
                else if (planet1.Weapons.Any(w => w.GetType().Name == "NuclearWeapon"))
                {
                    winner = 1;
                }
                else if (planet2.Weapons.Any(w => w.GetType().Name == "NuclearWeapon"))
                {
                    winner = 2;
                }
            }
            else if (planet1.MilitaryPower > planet2.MilitaryPower)
            {
                winner = 1;
            }
            else if (planet1.MilitaryPower < planet2.MilitaryPower)
            {
                winner = 2;
            }

            
            if (winner == 1)
            {
                planet1.Spend(planet1.Budget / 2);
                planet1.Profit(planet2.Budget / 2);

                double profit = planet2.Army.Sum(a => a.Cost) + planet2.Weapons.Sum(a => a.Price);
                planet1.Profit(profit);

                planets.RemoveItem(planet2.Name);

                return String.Format(OutputMessages.WinnigTheWar, planet1.Name, planet2.Name);
            }
            else if (winner == 2)
            {
                planet2.Spend(planet2.Budget / 2);
                planet2.Profit(planet1.Budget / 2);

                double profit = planet1.Army.Sum(a => a.Cost) + planet1.Weapons.Sum(a => a.Price);
                planet2.Profit(profit);

                planets.RemoveItem(planet1.Name);

                return String.Format(OutputMessages.WinnigTheWar, planet2.Name, planet1.Name);
            }
            else
            {
                planet1.Spend(planet1.Budget / 2);
                planet2.Spend(planet2.Budget / 2);

                return OutputMessages.NoWinner;
            }
        }

        public string SpecializeForces(string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planet.Army.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.NoUnitsFound);
            }

         
            planet.Spend(1.25);
            planet.TrainArmy();
            return String.Format(OutputMessages.ForcesUpgraded, planetName);
        }
    }
}
