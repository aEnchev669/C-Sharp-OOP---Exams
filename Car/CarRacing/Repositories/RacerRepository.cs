using CarRacing.Models.Racers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Repositories.Contracts
{
    public class RacerRepository : IRepository<IRacer>
    {
        public RacerRepository()
        {
            models = new List<IRacer>();
        }
        private  List<IRacer> models;
        public IReadOnlyCollection<IRacer> Models { get { return models; } }

        public void Add(IRacer model)
        {
            if (model == null)
            {
                throw new ArgumentException("Cannot add null in Car Repository");
            }
            models.Add(model);
        }

        public IRacer FindBy(string property)
        {
            return models.FirstOrDefault(c => c.Username == property);
        }

        public bool Remove(IRacer model)
        {
            return models.Remove(model);
        }
    }
}
