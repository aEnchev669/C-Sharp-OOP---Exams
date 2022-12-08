using Gym.Core.Contracts;
using Gym.Models.Athletes;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Core
{
    public class Controller : IController
    {
        public Controller()
        {
            equipments = new EquipmentRepository();
            gyms = new List<IGym>();
        }
        private EquipmentRepository equipments;
        private List<IGym> gyms;
        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            IAthlete athlete = null;
            IGym gym = gyms.First(g => g.Name == gymName);
            if (athleteType == "Boxer")
            {
                athlete = new Boxer(athleteName, motivation, numberOfMedals);
                if (gym is BoxingGym)
                {
                    gym.AddAthlete(athlete);
                }
                else
                {
                    return OutputMessages.InappropriateGym;
                }
            }
            else if (athleteType == "Weightlifter")
            {
                athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
                if (gym is WeightliftingGym)
                {
                    gym.AddAthlete(athlete);
                }
                else
                {
                    return OutputMessages.InappropriateGym;
                }
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
            }

            return String.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
        }

        public string AddEquipment(string equipmentType)
        {
            IEquipment equipment = null;
            if (equipmentType == "BoxingGloves")
            {
                equipment = new BoxingGloves();
            }
            else if (equipmentType == "Kettlebell")
            {
                equipment = new Kettlebell();
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentEquipment, equipmentType));
            }

            equipments.Add(equipment);
            return String.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string AddGym(string gymType, string gymName)
        {
            IGym gym = null;
            if (gymType == "BoxingGym")
            {
                gym = new BoxingGym(gymName);
            }
            else if (gymType == "WeightliftingGym")
            {
                gym = new WeightliftingGym(gymName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
            }

            gyms.Add(gym);
            return string.Format(OutputMessages.SuccessfullyAdded, gymType);
        }

        public string EquipmentWeight(string gymName)
        {
            IGym gym = gyms.First(g => g.Name == gymName);

            double allWeight = gym.EquipmentWeight;      //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            return String.Format(OutputMessages.EquipmentTotalWeight, gymName, allWeight);
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            IEquipment equipment = equipments.FindByType(equipmentType);
            if (equipment == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InexistentEquipment, equipmentType));
            }
            IGym gym = gyms.First(g => g.Name == gymName);

            gym.AddEquipment(equipment);
            equipments.Remove(equipment);
            return String.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var gym in gyms)
            {
                sb.AppendLine(gym.GymInfo());
            }
            return sb.ToString().TrimEnd();
        }

        public string TrainAthletes(string gymName)
        {
            IGym gym = gyms.First(g => g.Name == gymName);
            int count = 0;
            foreach (var athlete in gym.Athletes)
            {
                if (gym.GetType().Name == "BoxingGym")
                {
                    if (athlete.GetType().Name == "Boxer")
                    {
                        count++;
                        athlete.Exercise();
                    }
                }
                else if (gym.GetType().Name == "WeightliftingGym")
                {
                    if (athlete.GetType().Name == "Weightlifter")
                    {
                        count++;
                        athlete.Exercise();
                    }
                }
            }
            return $"Exercise athletes: {count}.";
        }
    }
}
