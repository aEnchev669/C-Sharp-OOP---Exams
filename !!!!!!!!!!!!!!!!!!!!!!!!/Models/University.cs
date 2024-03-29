﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Models
{
    public class University : IUniversity
    {
        public University(int universityId, string universityName, string category, int capacity, ICollection<int> requiredSubjects)
        {
            Id = universityId;
            Name = universityName;
            Category = category;
            Capacity = capacity;
            this.requiredSubjects = requiredSubjects.ToList();
        }
      //  public int Id { get; private set; }
        private int id;

        public int Id
        {
            get { return id; }
           private set { id = value; }
        }


        private string name;
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }
                name = value;
            }
        }
        private string category;

        public string Category
        {
            get { return category; }
            private set
            {
                if (value == "Technical")
                {
                    category = value;
                }
                else if (value == "Economical")
                {
                    category = value;
                }
                else if (value == "Humanity")
                {
                    category = value;
                }
                else
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.CategoryNotAllowed, value));
                }

            }
        }


        private int capacity;

        public int Capacity
        {
            get { return capacity; }
           private  set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.CapacityNegative);
                }
                capacity = value;
            }
        }

        private IReadOnlyCollection<int> requiredSubjects;

        public IReadOnlyCollection<int> RequiredSubjects => requiredSubjects;
    }
}
