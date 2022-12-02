using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public class Submarine : Vessel, ISubmarine
    {
        private const int Initial_Armor_Thickness = 200;
        private const int MainWeaponCaliberChange = 40;
        private const int SpeedChange = 4;
        public Submarine(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, Initial_Armor_Thickness)
        {
        }

        public bool SubmergeMode { get; private set; }

        public override void RepairVessel()
        {
            if (ArmorThickness < 200)
            {
                ArmorThickness = Initial_Armor_Thickness;
            }
        }

        public void ToggleSubmergeMode()
        {
            if (SubmergeMode == false)
            {
                MainWeaponCaliber += MainWeaponCaliberChange;
                Speed -= SpeedChange;
                
            }
            else
            {
                MainWeaponCaliber -= MainWeaponCaliberChange;
                Speed += SpeedChange;
                
            }

            SubmergeMode = !SubmergeMode;
        }

        public override string ToString()
        {

            string onOff = SubmergeMode == true ? "ON" : "OFF";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString())
               .AppendLine($" *Submerge mode: {onOff}");

            return sb.ToString().TrimEnd();
        }
    }
}
