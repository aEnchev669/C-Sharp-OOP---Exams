using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Booths
{
    public class Booth : IBooth
    {
        public Booth(int boothId, int capacity)
        {
            BoothId = boothId;
            Capacity = capacity;

            delicacyMenu = new DelicacyRepository();
            cocktailMenu = new CocktailRepository();
        }
        private DelicacyRepository delicacyMenu;
        private CocktailRepository cocktailMenu;
        public int BoothId { get;  set; }         //Private

        public int Capacity { get;  set; }

        public IRepository<IDelicacy> DelicacyMenu => delicacyMenu;

        public IRepository<ICocktail> CocktailMenu => cocktailMenu;

        public double CurrentBill { get;  set; } //!!!!!!!!!!

        public double Turnover { get;  set; }

        public bool IsReserved { get;  set; }        //

        public void ChangeStatus()
        {
            IsReserved = !IsReserved; // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        public void Charge()
        {
            Turnover = CurrentBill;
            CurrentBill = 0;
        }

        public void UpdateCurrentBill(double amount)
        {
            CurrentBill += amount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Booth: {BoothId}");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Turnover: {Turnover:f2} lv");
            sb.AppendLine($"-Cocktail menu:");
            foreach (var cocktail in cocktailMenu.Models)
            {
                sb.AppendLine($"--{cocktail}");         // withOut tostring
            }
            sb.AppendLine("-Delicacy menu:");
            foreach (var delicacy in delicacyMenu.Models)
            {
                sb.AppendLine($"--{delicacy}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
