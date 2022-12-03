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
            hotels = new List<IHotel>();
        }
        private  List<IHotel> hotels;
        public void AddNew(IHotel model) => hotels.Add(model);

        public IReadOnlyCollection<IHotel> All() => hotels.AsReadOnly();

        public IHotel Select(string criteria) => hotels.Find(h => h.GetType().Name == criteria);
    }
}
