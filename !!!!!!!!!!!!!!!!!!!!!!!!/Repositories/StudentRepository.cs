using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class StudentRepository : IRepository<IStudent>
    {
        public StudentRepository()
        {
            models = new List<IStudent>();
        }
        private List<IStudent> models;
        public IReadOnlyCollection<IStudent> Models => models;

        public void AddModel(IStudent model)
        {
            models.Add(model);
        }

        public IStudent FindById(int id)
        {
            return models.FirstOrDefault(m => m.Id == id);
        }

        public IStudent FindByName(string name)
        {
            string[] tokens = name.Split(" ");
            string fName = tokens[0];
            string lName = tokens[1];

            return models.FirstOrDefault(m => m.FirstName == fName && m.LastName == lName); //!!!!!!!!!!!!!!!!!!!!
        }
    }
}
