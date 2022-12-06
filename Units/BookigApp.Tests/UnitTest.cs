using FrontDeskApp;
using NUnit.Framework;
using System;

namespace BookigApp.Tests
{
    public class Tests
    {
        private Room defRoom;
        private Booking defBooking;
        private Hotel defHotel;
        [SetUp]
        public void Setup()
        {
            defRoom = new Room(10, 20);
            defBooking = new Booking(100, defRoom, 10);
            defHotel = new Hotel("Hotel", 2);
        }

        [Test]
        public void RoomCtorShouldSetValues()
        {
            int expectedCapacity = 10;
            int expectedPrice = 20;

            Room room = new Room(10, 20);

            Assert.AreEqual(expectedCapacity, room.BedCapacity);
            Assert.AreEqual(expectedPrice, room.PricePerNight);

        }
        [TestCase(0)]
        [TestCase(-50)]
        [TestCase(-1)]
        public void RoomBedCapacityShouldThrowException(int bedC)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Room room = new Room(bedC, 20);
            });
        }
        [TestCase(0)]
        [TestCase(-50)]
        [TestCase(-1)]
        public void RoomPriceShouldThrowException(int bedC)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Room room = new Room(20, bedC);
            });
        }
        [Test]
        public void BookingCtorShoudlWord()
        {
            int expected1 = 3;
            int expected2 = 10;
            Booking booking = new Booking(3, defRoom, 10);

            Assert.AreEqual(expected1, booking.BookingNumber);
            Assert.AreEqual(defRoom, booking.Room);
            Assert.AreEqual(expected2, booking.ResidenceDuration);
        }
        [Test]
        public void HotelCtorShouldWork()
        {
            string expectedName = "Naisss";
            int expectedCategory = 3;

            defHotel = new Hotel(expectedName, expectedCategory);

            Assert.AreEqual(expectedName, defHotel.FullName);
            Assert.AreEqual(expectedCategory, defHotel.Category);
        }
        [TestCase("")]
        [TestCase("    ")]
        [TestCase(null)]
        public void HotelFullNameShouldThrowException(string fullName)
        {
            string expectedName = fullName;
            int expectedCategory = 3;

            Assert.Throws<ArgumentNullException>(() =>
            {
                defHotel = new Hotel(expectedName, expectedCategory);
            });
        }
        [TestCase(0)]
        [TestCase(6)]
        [TestCase(1000)]
        [TestCase(-1)]
        [TestCase(-1000)]
        public void HotelCategoryShouldThrowException(int category)
        {


            Assert.Throws<ArgumentException>(() =>
            {
                defHotel = new Hotel("Party", category);
            });
        }
        [Test]
        public void HotelCollectionRooms()
        {
            defHotel.AddRoom(defRoom);
            defHotel.AddRoom(defRoom);
            defHotel.AddRoom(defRoom);

            int expectedCount = 3;

            Assert.AreEqual(expectedCount, defHotel.Rooms.Count);
        }
        [Test]
        public void HotelCollectionBookings()
        {
            defHotel.AddRoom(defRoom);

            defHotel.BookRoom(2, 3, 4, 100);

            int expectedCount = 1;

            Assert.AreEqual(expectedCount, defHotel.Bookings.Count);
        }
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void HotelShouldThrowExceptionAdults(int adults)
        {
            defHotel.AddRoom(defRoom);

            Assert.Throws<ArgumentException>(() =>
            {
                defHotel.BookRoom(adults, 3, 4, 100);
            });
        }
        
        [TestCase(-1)]
        [TestCase(-100)]
        public void HotelShouldThrowExceptionChildren(int child)
        {
            defHotel.AddRoom(defRoom);

            Assert.Throws<ArgumentException>(() =>
            {
                defHotel.BookRoom(3 , child, 4, 100);
            });
        }
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void HotelShouldThrowExceptionDuration(int duration)
        {
            defHotel.AddRoom(defRoom);

            Assert.Throws<ArgumentException>(() =>
            {
                defHotel.BookRoom(2, 3, duration, 100);
            });
        }
        [Test]
        public void HotelTurnOver()
        {
            defHotel.AddRoom(defRoom);
            defHotel.BookRoom(4, 2, 3, 1000);

            double ExpectedTurnover = 3 * defRoom.PricePerNight;

            Assert.AreEqual(ExpectedTurnover, defHotel.Turnover);
        }

    }
}