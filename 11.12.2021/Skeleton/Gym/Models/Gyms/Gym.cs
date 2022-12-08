using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Models.Gyms
{
    public abstract class Gym : IGym
    {
        public Gym(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;

            equipments = new List<IEquipment>();
            athletes = new List<IAthlete>();
        }
        private List<IEquipment> equipments;
        private List<IAthlete> athletes;

        private string name;

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGymName);
                }
                name = value;
            }
        }



        public int Capacity { get; }



        public double EquipmentWeight => equipments.Sum(e => e.Weight);

        public ICollection<IEquipment> Equipment => equipments;

        public ICollection<IAthlete> Athletes => athletes;

        public void AddAthlete(IAthlete athlete)
        {
            if (athletes.Count >= this.Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughSize);
            }
            athletes.Add(athlete);
        }

        public void AddEquipment(IEquipment equipment)
        {
            equipments.Add(equipment);
        }

        public void Exercise()
        {
            foreach (var athlete in athletes)
            {
                athlete.Exercise();
            }
        }

        public string GymInfo()
        {
            StringBuilder sb = new StringBuilder();
            string allAthlets = string.Empty;
            List<string> names = new List<string>();
            foreach (var athlete in athletes)
            {
                names.Add(athlete.FullName);
            }
            if (athletes.Count > 0)
            {
                allAthlets = string.Join(", ", names);
            }
            else
            {
                allAthlets = "No athletes";
            }

            sb.AppendLine($"{this.Name} is a {GetType().Name}:")
                .AppendLine($"Athletes: {allAthlets}")
                .AppendLine($"Equipment total count: {equipments.Count}")
                .AppendLine($"Equipment total weight: {this.EquipmentWeight:f2} grams");

            return sb.ToString().TrimEnd();
        }

        public bool RemoveAthlete(IAthlete athlete)
        {
            return athletes.Remove(athlete);
        }
    }
}
