using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChristmasPastryShop.Core.Contracts
{
    public class Controller : IController
    {
        public Controller()
        {
            boothRepository = new BoothRepository();
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1111111111111
        }
        
        private BoothRepository boothRepository;
        public string AddBooth(int capacity)
        {
            int boothId = boothRepository.Models.Count + 1;
            IBooth booth = new Booth(boothId, capacity);

            boothRepository.AddModel(booth);
            return String.Format(OutputMessages.NewBoothAdded, boothId, capacity);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            var booth = boothRepository.Models.FirstOrDefault(m => m.BoothId == boothId);

            //double multi = 0;
            //if (size == "Small")
            //{
            //    multi = 1 / 3 * 1;
            //}
            //else if (size == "Middle")
            //{
            //    multi = 2 / 3 * 1;
            //}
            if (size != "Middle" && size != "Large" && size != "Small")                                                     // maybe first should check for the type and then for the size!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                return String.Format(OutputMessages.InvalidCocktailSize, size);
            }
            ICocktail cocktail = booth.CocktailMenu.Models.FirstOrDefault(m => m.Name == cocktailName);
            if (cocktail != null)
            {
                return String.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }
            if (cocktailTypeName == "Hibernation")
            {
                cocktail = new Hibernation(cocktailName, size);
            }
            else if (cocktailTypeName == "MulledWine")
            {
                cocktail = new MulledWine(cocktailName, size);
            }
            else
            {
                return String.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }
            booth.CocktailMenu.AddModel(cocktail);

            return String.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            var booth = boothRepository.Models.FirstOrDefault(m => m.BoothId == boothId);

            IDelicacy delicacy = booth.DelicacyMenu.Models.FirstOrDefault(m => m.Name == delicacyName);                //!!!!!!!!!!!!!!!!!!!!!!!!!!!! can be wrong
            if (delicacy != null)
            {
                return String.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }
            if (delicacyTypeName == "Gingerbread")
            {
                delicacy = new Gingerbread(delicacyName);
            }
            else if (delicacyTypeName == "Stolen")
            {
                delicacy = new Stolen(delicacyName);
            }
            else
            {
                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }

            booth.DelicacyMenu.AddModel(delicacy);

            return String.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string BoothReport(int boothId)
        {
            var booth = boothRepository.Models.FirstOrDefault(m => m.BoothId == boothId);

            return booth.ToString().TrimEnd();
        }

        public string LeaveBooth(int boothId)
        {
            var booth = boothRepository.Models.FirstOrDefault(m => m.BoothId == boothId);

            booth.Charge();
            booth.ChangeStatus();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Bill {booth.Turnover:f2} lv")
                .AppendLine($"Booth {boothId} is now available!");

            return sb.ToString().TrimEnd();
        }

        public string ReserveBooth(int countOfPeople)
        {
            List<IBooth> boothsNotReserved = boothRepository.Models.Where(m => m.IsReserved == false).ToList();
            List<IBooth> booths = boothsNotReserved.Where(m => m.Capacity >= countOfPeople).ToList();

            if (booths.Count == 0)
            {
                return String.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }
            booths.OrderBy(t => t.Capacity).ThenByDescending(t => t.BoothId);

            IBooth booth = booths[0];
            Booth boothNew = (Booth)booth;
            boothNew.IsReserved = true;
            booth = boothNew;

            return String.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            var booth = boothRepository.Models.FirstOrDefault(m => m.BoothId == boothId);

            Booth boothNew = (Booth)booth;
            boothNew.IsReserved = true;
            booth = boothNew;

           
            string[] orders = order.Split('/').ToArray();
            string itemTypeName = orders[0].ToString();
            string itemName = orders[1].ToString();
            string count = orders[2];
            int countOfOrderPieces = int.Parse(count);
            string size = string.Empty;
            if (itemTypeName == "Hibernation" || itemTypeName == "MulledWine")       // or typename!!!!!!!!!
            {
                size = orders[3];
            }
            ICocktail cocktail = null;
            IDelicacy delicacy = null;
            if (itemTypeName == "Hibernation")
            {
                cocktail = booth.CocktailMenu.Models.FirstOrDefault(t=> t.Name == itemName);
                if (cocktail == null)
                {
                    return String.Format(OutputMessages.CocktailStillNotAdded, itemTypeName, itemName);
                }
                if (cocktail.Name == itemName && cocktail.Size == size )
                {
                    boothNew.CurrentBill += cocktail.Price * countOfOrderPieces;
                    booth = boothNew;
                    return String.Format(OutputMessages.SuccessfullyOrdered, boothId, countOfOrderPieces, itemName);
                }
                else
                {
                    return String.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                }
            }
            else if(itemTypeName == "MulledWine")
            {
                cocktail = booth.CocktailMenu.Models.FirstOrDefault(t => t.Name == itemName);
                if (cocktail == null)
                {
                    return String.Format(OutputMessages.CocktailStillNotAdded, itemTypeName, itemName);
                }
                if (cocktail.Name == itemName && cocktail.Size == size)
                {
                    boothNew.CurrentBill += cocktail.Price * countOfOrderPieces;
                    booth = boothNew;
                    return String.Format(OutputMessages.SuccessfullyOrdered, boothId, countOfOrderPieces, itemName);
                }
                else
                {
                    return String.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                }
            }

            else if (itemTypeName == "Gingerbread")
            {
                delicacy = booth.DelicacyMenu.Models.FirstOrDefault(t => t.Name == itemName);
                if (delicacy== null)
                {
                    return String.Format(OutputMessages.CocktailStillNotAdded, itemTypeName, itemName);
                }
                if (delicacy.Name == itemName )
                {
                    boothNew.CurrentBill += delicacy.Price * countOfOrderPieces;
                    booth = boothNew;
                    return String.Format(OutputMessages.SuccessfullyOrdered, boothId, countOfOrderPieces, itemName);
                }
                else
                {
                    return String.Format(OutputMessages.DelicacyStillNotAdded, itemTypeName, itemName);
                }
            }
            else if (itemTypeName == "Stolen")
            {
                delicacy = booth.DelicacyMenu.Models.FirstOrDefault(t => t.Name == itemName);
                if (delicacy == null)
                {
                    return String.Format(OutputMessages.CocktailStillNotAdded, itemTypeName, itemName);
                }
                if (delicacy.Name == itemName)
                {
                    boothNew.CurrentBill += delicacy.Price * countOfOrderPieces;
                    booth = boothNew;
                    return String.Format(OutputMessages.SuccessfullyOrdered, boothId, countOfOrderPieces, itemName);
                }
                else
                {
                    return String.Format(OutputMessages.DelicacyStillNotAdded, itemTypeName, itemName);
                }
            }
            else
            {
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);
            }

        }
    }
}
