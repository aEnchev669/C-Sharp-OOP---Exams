using BookingApp.Models.Bookings;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingApp.Models.Rooms
{
    public class Apartment : Room
    {
        private const int ApartmentBedCapacity = 6;

        public Apartment() : base(ApartmentBedCapacity)
        {
        }
    }
}
