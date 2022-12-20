using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class SubjectRepository : IRepository<ISubject>
    {
        public SubjectRepository()
        {
            models = new List<ISubject>();
        }
        private List<ISubject> models;
        public IReadOnlyCollection<ISubject> Models => models;

        public void AddModel(ISubject model)
        {
            models.Add(model);
        }

        public ISubject FindById(int id)
        {
            return models.FirstOrDefault(m => m.Id == id);
        }

        public ISubject FindByName(string name)
        {
            return models.FirstOrDefault(m => m.Name == name);
        }
    }
}
