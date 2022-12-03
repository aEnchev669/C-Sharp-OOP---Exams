using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
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
        private HotelRepository hotels;
        public string AddHotel(string hotelName, int category)
        {
            if (hotels.All().Any(h => h.FullName == hotelName))
            {
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
            }
            IHotel hotel = new Hotel(hotelName, category);
            hotels.AddNew(hotel);

            return string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);

        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            if (!hotels.All().Any(h => h.Category == category))
            {
                return String.Format(OutputMessages.CategoryInvalid, category);
            }

            Dictionary<IRoom, string> availibeRooms = new Dictionary<IRoom, string>();
            foreach (var hotel in hotels.All().Where(h => h.Category == category).OrderBy(h => h.FullName))
            {
                foreach (var room in hotel.Rooms.All())
                {
                    if (room.PricePerNight > 0)
                    {
                        availibeRooms.Add(room, hotel.FullName);
                    }
                }
            }

            IRoom booking = null;
            int guests = adults + children;
            string hotelNameBooking = string.Empty;

            foreach (var item in availibeRooms.OrderBy(r => r.Key.BedCapacity))
            {
                if (item.Key.BedCapacity > guests)
                {
                    booking = item.Key;
                    hotelNameBooking = item.Value;
                    break;
                }
            }

            if (booking == null)
            {
                return OutputMessages.RoomNotAppropriate;
            }

            IHotel hotelBook = hotels.Select(hotelNameBooking);
            int bookingNumber = hotelBook.Bookings.All().Count + 1;
            Booking succBooking = new Booking(booking, duration, adults, children, bookingNumber);

            hotelBook.Bookings.AddNew(succBooking);

            return String.Format(OutputMessages.BookingSuccessful, bookingNumber, hotelNameBooking);
        }

        public string HotelReport(string hotelName)
        {
            StringBuilder sb = new StringBuilder();
            if (hotels.All().Any(h => h.FullName == hotelName))
            {
                return String.Format(OutputMessages.HotelNameInvalid, hotelName);
            }
            IHotel hotel = hotels.Select(hotelName);

            sb.AppendLine($"Hotel name: {hotelName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:f2} $");
            sb.AppendLine($"--Bookings:");
            sb.AppendLine();

            if (hotel.Bookings.All().Count == 0)
            {
                sb.AppendLine("none");
                return sb.ToString().TrimEnd();
            }

            foreach (var booking in hotel.Bookings.All())
            {
                sb.AppendLine(booking.BookingSummary());
            }

            return sb.ToString().TrimEnd();

        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            if (!hotels.All().Any(h => h.FullName == hotelName))
            {
                return String.Format(OutputMessages.HotelNameInvalid, hotelName);
            }
            if (roomTypeName != "Apartment" && roomTypeName != "Studio" && roomTypeName != "DoubleBed")
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }
            IHotel hotel = hotels.All().First(h => h.FullName == hotelName);
            bool isCreated = hotel.Rooms.All().Any(r => r.GetType().Name == roomTypeName);
            if (!isCreated)
            {
                return OutputMessages.RoomTypeNotCreated;
            }

            IRoom room = hotel.Rooms.All().First(r => r.GetType().Name == roomTypeName);
            if (room.PricePerNight != 0)
            {
                throw new InvalidOperationException(ExceptionMessages.PriceAlreadySet);
            }

            room.SetPrice(price);
            return string.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);

        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            if (!hotels.All().Any(h => h.FullName == hotelName))
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            IHotel hotel = hotels.All().First(h => h.FullName == hotelName);
            bool alreadyCreated = hotel.Rooms.All().Any(r => r.GetType().Name == roomTypeName);
            if (alreadyCreated)
            {
                return (OutputMessages.RoomTypeAlreadyCreated);
            }
            IRoom roomtoAdd;

            if (roomTypeName == "Apartment")
            {
                roomtoAdd = new Apartment();
            }
            else if (roomTypeName == "DoubleBed")
            {
                roomtoAdd = new DoubleBed();
            }
            else if (roomTypeName == "Studio")
            {
                roomtoAdd = new Studio();
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }

            hotel.Rooms.AddNew(roomtoAdd);
            return String.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);

        }
    }
}
