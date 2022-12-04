using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        public WeaponRepository()
        {
            weapons = new List<IWeapon>();
        }
        private readonly List<IWeapon> weapons;
        public IReadOnlyCollection<IWeapon> Models => weapons.AsReadOnly();

        public void AddItem(IWeapon model)
        {
            weapons.Add(model);
        }

        public IWeapon FindByName(string name)
        {
            return weapons.FirstOrDefault(w => w.GetType().Name == name);
        }

        public bool RemoveItem(string name)
        {
            return weapons.Remove(weapons.Find(w => w.GetType().Name == name));
        }
    }
}
