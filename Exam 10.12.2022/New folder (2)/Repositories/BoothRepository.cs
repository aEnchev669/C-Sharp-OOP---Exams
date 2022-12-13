using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Repositories
{
    public class BoothRepository : IRepository<IBooth>
    {
        public BoothRepository()
        {
            models = new List<IBooth>();
        }
        private List<IBooth> models;
        public IReadOnlyCollection<IBooth> Models => models;

       

        public void AddModel(IBooth model)
        {
            models.Add(model);
        }
    }
}
