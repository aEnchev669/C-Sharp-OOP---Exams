using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingApp.Repositories
{
    public class HotelRepository : IRepository<IHotel>
    {
        public HotelRepository()
        {
            bookings = new List<IHotel>();
        }
        private List<IHotel> bookings;
        public void AddNew(IHotel model)
        {
            bookings.Add(model);
        }

        public IReadOnlyCollection<IHotel> All()
        {
            return bookings.AsReadOnly();
        }

        public IHotel Select(string criteria)
        {
            return bookings.FirstOrDefault(b => b.FullName == criteria);
        }
    }
}
