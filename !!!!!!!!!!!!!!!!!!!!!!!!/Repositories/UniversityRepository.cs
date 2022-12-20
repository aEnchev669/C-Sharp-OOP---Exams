﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class UniversityRepository : IRepository<IUniversity>
    {
        public UniversityRepository()
        {
            models = new List<IUniversity>();
        }
        private List<IUniversity> models;
        public IReadOnlyCollection<IUniversity> Models => models;

        public void AddModel(IUniversity model)
        {
            models.Add(model);
        }

        public IUniversity FindById(int id)
        {
            return models.FirstOrDefault(m => m.Id == id);
        }

        public IUniversity FindByName(string name)
        {
            return models.FirstOrDefault(m => m.Name == name);

        }
    }
}
