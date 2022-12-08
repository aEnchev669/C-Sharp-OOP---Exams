using Easter.Models.Bunnies.Contracts;
using Easter.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Repositories
{
    public class BunnyRepository : IRepository<IBunny>
    {
        public BunnyRepository()
        {
            models = new List<IBunny>();
        }
        private  List<IBunny> models;               
        public IReadOnlyCollection<IBunny> Models => models.AsReadOnly();

        public void Add(IBunny model)
        {
            models.Add(model);
        }

        public IBunny FindByName(string name)
        {
            return models.FirstOrDefault(n => n.Name == name);
        }

        public bool Remove(IBunny model)
        {
            return models.Remove(model);
        }
    }
}
