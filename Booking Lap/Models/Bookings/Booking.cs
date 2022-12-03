using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingApp.Models.Bookings
{
    public class Booking : IBooking
    {

        public Booking(IRoom room, int residenceDuration, int adultsCount, int childrenCount, int bookingNumber)
        {
            this.Room = room;
            this.ResidenceDuration = residenceDuration;
            this.AdultsCount = adultsCount;
            this.ChildrenCount = childrenCount;
            this.BookingNumber = bookingNumber;
        }
        public IRoom Room { get; private set; }

        private int residenceDuration;

        public int ResidenceDuration
        {
            get { return residenceDuration; }
           private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.DurationZeroOrLess);
                }
                residenceDuration = value;
            }
        }


        private int adultsCount;

        public int AdultsCount
        {
            get { return adultsCount; }
          private  set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.AdultsZeroOrLess);
                }
                adultsCount = value;
            }
        }


        private int childrenCount;

        public int ChildrenCount
        {
            get { return childrenCount; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.ChildrenNegative);
                }
                childrenCount = value;
            }
        }
        

        public int BookingNumber { get; private set; }
        public string BookingSummary()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Booking number: {BookingNumber}")
                .AppendLine($"Room type: {Room.GetType()}")
                .AppendLine($"Adults: {AdultsCount} Children: {ChildrenCount}")
                .AppendLine($"Total amount paid: {TotalPaid():F2}");

            return sb.ToString().TrimEnd();
        }
        private double TotalPaid() => Math.Round(ResidenceDuration * Room.PricePerNight, 2);
    }
}
