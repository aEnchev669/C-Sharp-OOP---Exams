using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public class Battleship : Vessel, IBattleship
    {
        private const int Initial_Armor_Thickness = 300;
        private const int MainWeaponCaliberChange = 40;
        private const int SpeedChange = 5;
        public Battleship(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, Initial_Armor_Thickness)
        {
        }

        public bool SonarMode { get; private set; }

        public override void RepairVessel()
        {
            if (ArmorThickness < 300)
            {
                ArmorThickness = Initial_Armor_Thickness;
            }
        }

        public void ToggleSonarMode()
        {
            if (SonarMode == false)
            {
                MainWeaponCaliber += MainWeaponCaliberChange;
                Speed -= SpeedChange;
                
            }
            else
            {
                MainWeaponCaliber -= MainWeaponCaliberChange;
                Speed += SpeedChange;
                
            }
            SonarMode = !SonarMode;
        }

        public override string ToString()
        {
            string onOff = SonarMode == true ? "ON" : "OFF";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString())
               .AppendLine($" *Sonar mode: {onOff}");

            return sb.ToString().TrimEnd(); 
        }
    }
}
