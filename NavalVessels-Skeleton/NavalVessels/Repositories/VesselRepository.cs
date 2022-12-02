using NavalVessels.Models.Contracts;
using NavalVessels.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavalVessels.Repositories
{
    public class VesselRepository : IRepository<IVessel>
    {
        public VesselRepository()
        {
            models = new List<IVessel>();
        }
        private readonly List<IVessel> models;
        public IReadOnlyCollection<IVessel> Models => this.models.AsReadOnly();

        public void Add(IVessel model)
        {
            models.Add(model);
        }

        public IVessel FindByName(string name)
        {
            return models.FirstOrDefault(v => v.Name == name);
        }

        public bool Remove(IVessel model)
        {
            return models.Remove(model);
        }
    }
}
