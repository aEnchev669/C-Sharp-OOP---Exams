using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Cocktails
{
    public abstract class Cocktail : ICocktail
    {
        public Cocktail(string cocktailName, string size, double price)
        {
            Name = cocktailName;
            Size = size;
            Price = price;

        }
        private string name;

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }
                name = value;
            }
        }

        private string size;

        public string Size
        {                                                    //May be without field
            get { return size; }
            private set { size = value; }
        }


        private double price;

        public double Price
        {
            get { return price; }                                      //May be without field and can do matchCalculation here
            private set
            {
                if (Size == "Small")
                {
                    price = (value / 3);
                }
                else if (Size == "Middle")
                {
                    price = value / 3 * 2;
                }
                else
                {
                    price = value;

                }
            }
        }

        public override string ToString()
        {
            return $"{Name} ({Size}) - {Price:f2} lv";
        }

    }
}
