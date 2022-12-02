using NavalVessels.Core.Contracts;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavalVessels.Core
{
    public class Controller : IController
    {
        private const double UnarmoredVesselArmorThickness = 0;
        public Controller()
        {
            vessels = new VesselRepository();
            captains = new List<ICaptain>();
        }
        private readonly VesselRepository vessels;
        private readonly ICollection<ICaptain> captains;

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            ICaptain captain = captains.FirstOrDefault(c => c.FullName == selectedCaptainName);

            if (captain == null)
            {
                return String.Format(OutputMessages.CaptainNotFound, selectedCaptainName);
            }

            IVessel vessel = vessels.FindByName(selectedVesselName);
            if (vessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, selectedVesselName);
            }
            if (vessel.Captain != null)
            {
                return String.Format(OutputMessages.VesselOccupied, selectedVesselName);
            }


            captain.AddVessel(vessel);
            vessel.Captain = captain;

            return String.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            IVessel attackingVessel = vessels.FindByName(attackingVesselName);
            if (attackingVessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, attackingVesselName);
            }

            IVessel defendingVessel = vessels.FindByName(defendingVesselName);
            if (defendingVessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, defendingVesselName);
            }
            if (attackingVessel.ArmorThickness == UnarmoredVesselArmorThickness)
            {
                return String.Format(OutputMessages.AttackVesselArmorThicknessZero, attackingVesselName);
            }
            if (defendingVessel.ArmorThickness == UnarmoredVesselArmorThickness)
            {
                return String.Format(OutputMessages.AttackVesselArmorThicknessZero, defendingVesselName);
            }

            attackingVessel.Attack(defendingVessel);
            return String.Format(OutputMessages.SuccessfullyAttackVessel, defendingVesselName, attackingVesselName, defendingVessel.ArmorThickness);
        }

        public string CaptainReport(string captainFullName)
        {
            ICaptain captain = captains.First(c => c.FullName == captainFullName);

            return captain.Report();
        }

        public string HireCaptain(string fullName)
        {
            ICaptain captain = new Captain(fullName);

            if (this.captains.Any(c => c.FullName == fullName))
            {
                return string.Format(OutputMessages.CaptainIsAlreadyHired, fullName);
            }

            this.captains.Add(captain);
            return string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            IVessel vessel = vessels.FindByName(name);

            if (vessel != null)
            {
                return (string.Format(OutputMessages.VesselIsAlreadyManufactured, vesselType, name));
            }
            if (vesselType == "Submarine")
            {
                vessel = new Submarine(name, mainWeaponCaliber, speed);
            }
            else if (vesselType == "Battleship")
            {
                vessel = new Battleship(name, mainWeaponCaliber, speed);
            }
            else
            {
                return (OutputMessages.InvalidVesselType);
            }

            vessels.Add(vessel);
            return string.Format(OutputMessages.SuccessfullyCreateVessel, vesselType, name, mainWeaponCaliber, speed);
        }

        public string ServiceVessel(string vesselName)
        {
            IVessel vessel = vessels.FindByName(vesselName);
            if (vessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, vesselName);
            }

            vessel.RepairVessel();
            return String.Format(OutputMessages.SuccessfullyRepairVessel, vesselName);

        }

        public string ToggleSpecialMode(string vesselName)
        {
            IVessel vessel = vessels.FindByName(vesselName);
            if (vessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, vesselName);
            }
            if (vessel.GetType() == typeof(Battleship))                            // Can i say "Battleship" instead typeof(Battleship)?!!!!!!!!!!!!!
            {
                Battleship battleship = (Battleship)vessel;
                battleship.ToggleSonarMode();

                return String.Format(OutputMessages.ToggleBattleshipSonarMode, vesselName);
            }
            else
            {
                Submarine submarine = (Submarine)vessel;
                submarine.ToggleSubmergeMode();

                return String.Format(OutputMessages.ToggleSubmarineSubmergeMode, vesselName);
            }
        }

        public string VesselReport(string vesselName)
        {
            IVessel vessel = vessels.FindByName(vesselName);

            return vessel?.ToString();
        }
    }
}
