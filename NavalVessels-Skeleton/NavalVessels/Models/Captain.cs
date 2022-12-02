using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavalVessels.Models
{
    public class Captain : ICaptain
    {
        private const int CombatExperienceIncreaseStep = 10;
        public Captain()
        {
            Vessels = new List<IVessel>();

        }
        public Captain(string fullName): this()
        {
            FullName = fullName;
        }
        private string fullName;

        public string FullName
        {
            get { return fullName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidCaptainName);
                }
                fullName = value;
            }
        }



        public int CombatExperience { get; private set; }

        public ICollection<IVessel> Vessels { get; private set; }

        public void AddVessel(IVessel vessel)
        {

            if (vessel == null)
            {
                throw new NullReferenceException(ExceptionMessages.InvalidVesselForCaptain);
            }
            Vessels.Add(vessel);
        }

        public void IncreaseCombatExperience()
        {
            CombatExperience += CombatExperienceIncreaseStep;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{FullName} has {CombatExperience} combat experience and commands {Vessels.Count} vessels.");

            foreach (IVessel vessel in this.Vessels)
            {
                sb
                    .AppendLine(vessel.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
