using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        public PlanetRepository()
        {
            planets = new List<IPlanet>();
        }
        private readonly List<IPlanet> planets;
        public IReadOnlyCollection<IPlanet> Models => planets.AsReadOnly();

        public void AddItem(IPlanet model)
        {
            planets.Add(model);
        }

        public IPlanet FindByName(string name)
        {
            return planets.FirstOrDefault(w => w.GetType().Name == name);
        }

        public bool RemoveItem(string name)
        {
            return planets.Remove(planets.Find(w => w.GetType().Name == name));
        }
    }
}
