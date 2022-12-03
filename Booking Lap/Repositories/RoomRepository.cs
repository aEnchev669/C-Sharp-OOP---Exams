using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingApp.Repositories
{
    public class RoomRepository : IRepository<IRoom>
    {
        public RoomRepository()
        {
            rooms = new List<IRoom>();
        }
        private  List<IRoom> rooms;
        public void AddNew(IRoom model) => rooms.Add(model);


        public IReadOnlyCollection<IRoom> All() => rooms.AsReadOnly();


        public IRoom Select(string criteria) => rooms.Find(r => r.GetType().Name == criteria);                      

    }
}
