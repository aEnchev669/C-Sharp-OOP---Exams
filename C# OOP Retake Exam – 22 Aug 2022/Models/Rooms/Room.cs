using BookingApp.Models.Rooms.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingApp.Models.Rooms
{
    public abstract class Room : IRoom
    {
        public Room(int bedCapacity)
        {
            BedCapacity = bedCapacity;
        }
        public int BedCapacity { get; private set; } //!!!!!!!!!!!!!!!!!!!

        private double pricePerNight;

        public double PricePerNight
        {
            get { return pricePerNight; }
            private set
            {
                if (value < 0 )
                {
                    throw new ArgumentException(ExceptionMessages.PricePerNightNegative);
                }
                pricePerNight = value;
            }
        }



        public void SetPrice(double price)
        {
            PricePerNight = price;
        }
    }
}
