using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Repositories;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Core
{
    public class Controller : IController
    {
        public Controller()
        {
            bunnies = new BunnyRepository();
            eggs = new EggRepository();
        }
        private BunnyRepository bunnies;
        private EggRepository eggs;
        public string AddBunny(string bunnyType, string bunnyName)
        {
            IBunny bunny = null;
            if (bunnyType == "HappyBunny")
            {
                bunny = new HappyBunny(bunnyName);
            }
            else if (bunnyType == "SleepyBunny")
            {
                bunny = new SleepyBunny(bunnyName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }
            bunnies.Add(bunny);
            return String.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            Dye dye = new Dye(power);
            IBunny bunny = bunnies.FindByName(bunnyName);
            if (bunny == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }
            bunny.AddDye(dye);

            return String.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            IEgg egg = new Egg(eggName, energyRequired);

            eggs.Add(egg);

            return String.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            IEgg egg = eggs.FindByName(eggName);
            List<IBunny> selectedBunnies = new List<IBunny>();
            foreach (var bunny in bunnies.Models)
            {
                if (bunny.Energy >= 50)
                {
                    selectedBunnies.Add(bunny);
                }
            }
            if (!selectedBunnies.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
            }
            selectedBunnies.OrderByDescending(e => e.Energy);
            foreach (var bunny in selectedBunnies)
            {
                while (bunny.Energy > 0)
                {
                    egg.GetColored();
                    if (egg.EnergyRequired == 0)
                    {
                        break;
                    }
                    bunny.Work();
                }
                if (bunny.Energy == 0)
                {
                    bunnies.Remove(bunny);
                }
                if (egg.EnergyRequired == 0)
                {
                    break;
                }
            }
            if (egg.EnergyRequired == 0)
            {
                return string.Format(OutputMessages.EggIsDone, eggName);
            }
            return string.Format(OutputMessages.EggIsNotDone, eggName);

        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            List<IEgg> coloredEggs = eggs.Models.Where(i => i.EnergyRequired == 0).ToList();

            sb.AppendLine($"{coloredEggs.Count} eggs are done!");
            sb.AppendLine("Bunnies info:");

            foreach (var bunny in bunnies.Models)
            {
                sb.AppendLine($"Name: {bunny.Name}")
                    .AppendLine($"Energy: {bunny.Energy}")
                    .AppendLine($"Dyes: {bunny.Dyes.Count} not finished");
            }
            
            return sb.ToString().TrimEnd();
        }
    }
}
