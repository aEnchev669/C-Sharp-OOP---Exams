﻿using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        public WeaponRepository()
        {
            weapons = new List<IWeapon>();
        }
        private List<IWeapon> weapons;
        public IReadOnlyCollection<IWeapon> Models => weapons;

        public void Add(IWeapon model)
        {
            weapons.Add(model);
        }

        public IWeapon FindByName(string name)
        {
            return weapons.FirstOrDefault(w => w.Name == name);
        }

        public bool Remove(IWeapon model)
        {
            return weapons.Remove(model);
        }
    }
}
