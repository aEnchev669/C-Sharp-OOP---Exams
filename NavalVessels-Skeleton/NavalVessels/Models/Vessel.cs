using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavalVessels.Models.Contracts
{
    public abstract class Vessel : IVessel
    {
        public Vessel()
        {
            Targets = new List<string>();

        }

        protected Vessel(string name, double mainWeaponCaliber, double speed, double armorThickness) : this()
        {
            Name = name;
            MainWeaponCaliber = mainWeaponCaliber;
            Speed = speed;
            ArmorThickness = armorThickness;

        }
        private string name;

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidVesselName);
                }
                name = value;
            }
        }

        private ICaptain captain;

        public ICaptain Captain
        {
            get { return captain; }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException(ExceptionMessages.InvalidCaptainName);
                }
                captain = value;
            }
        }
        public double ArmorThickness { get; set; }
        public double MainWeaponCaliber { get; protected set; }

        public double Speed { get; protected set; }

        public ICollection<string> Targets { get; private set; }

        public void Attack(IVessel target)
        {
            if (target == null)
            {
                throw new NullReferenceException(ExceptionMessages.InvalidTarget);
            }
            target.ArmorThickness -= MainWeaponCaliber;
            if ( target.ArmorThickness < 0)
            {
                target.ArmorThickness = 0;
            }
            Targets.Add(target.Name);

            this.Captain.IncreaseCombatExperience();
            target.Captain.IncreaseCombatExperience();
        }

        public abstract void RepairVessel();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            string targets = Targets.Any() ? string.Join(", ", Targets) : "None";
            
            sb.AppendLine($"- {Name}")
                .AppendLine($" *Type: {GetType().Name}")
                .AppendLine($" *Armor thickness: {ArmorThickness}")
                .AppendLine($" *Main weapon caliber: {MainWeaponCaliber}")
                .AppendLine($" *Speed: {Speed} knots")
                .AppendLine($" *Targets: {targets}");

            return sb.ToString().TrimEnd();
        }
    }
}
