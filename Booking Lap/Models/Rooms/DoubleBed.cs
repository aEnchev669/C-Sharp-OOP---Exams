using BookingApp.Models.Bookings;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingApp.Models.Rooms
{
    public class DoubleBed : Room
    {
        private const int DoubleBedCapacity = 2;
        public DoubleBed() : base(DoubleBedCapacity)
        {
        }
    }
}
