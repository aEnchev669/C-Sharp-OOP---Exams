using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        public Controller()
        {
            hotels = new HotelRepository();
        }
        private IRepository<IHotel> hotels;
        public string AddHotel(string hotelName, int category)
        {
            IHotel hotel = hotels.Select(hotelName);
            if (hotel != null)
            {
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
            }

            hotel = new Hotel(hotelName, category);
            hotels.AddNew(hotel);
            return String.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            if (!hotels.All().Any(h => h.Category == category))
            {
                return String.Format(OutputMessages.CategoryInvalid, category);
            }

            Dictionary<IRoom, string> rooms = new Dictionary<IRoom, string>();

            foreach (var item in hotels.All().Where(h => h.Category == category).OrderBy(h => h.FullName))
            {
                foreach (var room in item.Rooms.All())
                {
                    if (room.PricePerNight > 0)
                    {
                        rooms.Add(room, item.FullName);
                    }
                }
            }

            IRoom curRoom = null;
            string hotelName = string.Empty;
            int guests = adults + children;

            foreach (var item in rooms.OrderBy(r => r.Key.BedCapacity))
            {
                if (item.Key.BedCapacity >= guests)
                {
                    curRoom = item.Key;
                    hotelName = item.Value;
                    break;
                }
            }

            if (curRoom == null)
            {
                return OutputMessages.RoomNotAppropriate;
            }

            IHotel hotel = hotels.Select(hotelName);
            int bookingNumber = hotel.Bookings.All().Count() + 1;
            IBooking booking = new Booking(curRoom, duration, adults, children, bookingNumber);

            hotel.Bookings.AddNew(booking);

            return String.Format(OutputMessages.BookingSuccessful, bookingNumber, hotelName);
        }

        public string HotelReport(string hotelName)
        {
            IHotel hotel = hotels.Select(hotelName);
            if (hotel == null)
            {
                return String.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Hotel name: {hotelName}")
                .AppendLine($"--{hotel.Category} star hotel")
                .AppendLine($"--Turnover: {hotel.Turnover:f2} $")
                .AppendLine($"--Bookings:");


            if (hotel.Bookings.All().Count == 0)
            {
                sb.AppendLine();
                sb.AppendLine("none");
                return sb.ToString().TrimEnd();
            }
            foreach (var item in hotel.Bookings.All())
            {
                sb.AppendLine(item.BookingSummary());
            }

            return sb.ToString().TrimEnd();
        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            IHotel hotel = hotels.Select(hotelName);
            if (hotel == null)
            {
                return String.Format(OutputMessages.HotelNameInvalid, hotelName);
            }


            if (roomTypeName != "DoubleBed" && roomTypeName != "Apartment" && roomTypeName != "Studio")
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }

            IRoom room = hotel.Rooms.Select(roomTypeName);
            if (room == null)
            {
                return OutputMessages.RoomTypeNotCreated;
            }

            if (room.PricePerNight != 0)
            {
                throw new InvalidOperationException(ExceptionMessages.PriceAlreadySet);
            }

            room.SetPrice(price);

            return String.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);
        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            IHotel hotel = hotels.Select(hotelName);
            if (hotel == null)
            {
                return String.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            if (hotel.Rooms.Select(roomTypeName) != null)
            {
                return OutputMessages.RoomTypeAlreadyCreated;
            }
            IRoom room = null;
            if (roomTypeName == "DoubleBed")
            {
                room = new DoubleBed();
            }
            else if (roomTypeName == "Apartment")
            {
                room = new Apartment();
            }
            else if (roomTypeName == "Studio")
            {
                room = new Studio();
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }

            hotel.Rooms.AddNew(room);

            return String.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
        }
    }
}
