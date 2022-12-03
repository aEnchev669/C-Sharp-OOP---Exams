using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingApp.Models.Hotels
{
    public class Hotel : IHotel
    {
        public Hotel(string fullName, int category)
        {
            FullName = fullName;
            Category = category;

            rooms = new RoomRepository();
            bookings = new BookingRepository();
        }
        private string fullName;

        public string FullName
        {
            get { return fullName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.HotelNameNullOrEmpty);
                }
                fullName = value;
            }
        }


        private int category;

        public int Category
        {
            get { return category; }
            private set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCategory);
                }
                category = value;
            }
        }


        private double turnover;

        public double Turnover
        {
            get
            {
                turnover = 0;
                foreach (var booking in Bookings.All())
                {
                    turnover += booking.ResidenceDuration * booking.Room.PricePerNight;
                }
                return turnover;
            }
        }


        private IRepository<IRoom> rooms;

        public IRepository<IRoom> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }

        private IRepository<IBooking> bookings;

        public IRepository<IBooking> Bookings
        {
            get { return bookings; }
            set { bookings = value; }
        }
    }
}
