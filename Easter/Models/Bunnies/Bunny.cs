using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easter.Models
{
    public abstract class Bunny : IBunny
    {
       
        public Bunny(string name, int energy)
        {
            Name = name;
            Energy = energy;

            dyes = new List<IDye>();
        }
        private string name;

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBunnyName);
                }
                name = value;
            }
        }

        private int energy;

        public int Energy
        {
            get { return energy; }
            protected set
            {
                if (value < 0)
                {
                    value = 0;
                }
                energy = value;
            }
        }

        private List<IDye> dyes;

        public ICollection<IDye> Dyes => dyes;



        public void AddDye(IDye dye)
        {
            Dyes.Add(dye);
        }

        public void Work()
        {
            Energy -=15;
        }
    }
}
