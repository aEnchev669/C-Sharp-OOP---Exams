using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        public Aquarium(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;

            decorations = new List<IDecoration>();
            fishes = new List<IFish>();
        }
        private List<IDecoration> decorations;
        private List<IFish> fishes;

        private string name;

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }
                name = value;
            }
        }



        public int Capacity { get; private set; }  //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! protected or only with getter

        public int Comfort => decorations.Sum(d => d.Comfort);


        public ICollection<IDecoration> Decorations => decorations;

        public ICollection<IFish> Fish => fishes;

        public void AddDecoration(IDecoration decoration)
        {
            decorations.Add(decoration);
        }

        public void AddFish(IFish fish)
        {
            if (fishes.Count >= Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }

            fishes.Add(fish);
        }

        public void Feed()
        {
            if (fishes.Any())
            {
                foreach (var fish in fishes)
                {
                    fish.Eat();
                }
            }
        }

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            List<string> fishesNames = new List<string>();
            foreach (var fish in fishes)
            {
                fishesNames.Add(fish.Name);
            }
            string allFishes = fishes.Any() ? string.Join(", ",fishesNames ) : "none";
            sb.AppendLine($"{Name} ({GetType().Name}):")
                .AppendLine($"Fish: {allFishes}")
                .AppendLine($"Decorations: {decorations.Count}")
                .AppendLine($"Comfort: {Comfort}");

            return sb.ToString().TrimEnd();
        }

        public bool RemoveFish(IFish fish)
        {
            return fishes.Remove(fish);
        }
    }
}
