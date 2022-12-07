using CarRacing.Models.Cars.Contracts;
using CarRacing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Repositories
{
    public class CarRepository : IRepository<ICar>
    {
        public CarRepository()
        {
            models = new List<ICar>();
        }
        private  List<ICar> models;                    
        public IReadOnlyCollection<ICar> Models { get { return models; } }

        public void Add(ICar model)
        {
            if (model == null)
            {
                throw new ArgumentException("Cannot add null in Car Repository");
            }
            models.Add(model);
        }

        public ICar FindBy(string property)
        {
            return models.FirstOrDefault(c => c.VIN == property);
        }

        public bool Remove(ICar model)
        {
            return models.Remove(model);
        }
    }
}
