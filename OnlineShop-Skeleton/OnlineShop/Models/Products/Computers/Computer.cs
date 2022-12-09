using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Models.Products.Computers
{
    public abstract class Computer : Product, IComputer
    {
        private List<IComponent> components = new List<IComponent>();
        private List<IPeripheral> peripherals = new List<IPeripheral>();
        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance) : base(id, manufacturer, model, price, overallPerformance)
        {
        }

        public IReadOnlyCollection<IComponent> Components => components;

        public IReadOnlyCollection<IPeripheral> Peripherals => peripherals;
        public override double OverallPerformance
        {
            get
            {
                if (components.Count == 0)
                {
                    return base.OverallPerformance;
                }
                return base.OverallPerformance + components.Average(x => x.OverallPerformance);
            }
        }

        public override decimal Price
        {
            get
            {
                return base.Price + components.Sum(x => x.Price) + peripherals.Sum(x => x.Price);
            }
        }

        public void AddComponent(IComponent component)
        {
            if (components.Any(x => x.GetType().Name == component.GetType().Name))
            {
                throw new ArgumentException(String.Format(ExceptionMessages.ExistingComponent, component.GetType().Name, GetType().Name, Id));
            }
            components.Add(component);
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            if (peripherals.Any(x => x.GetType().Name == peripheral.GetType().Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingPeripheral, peripheral.GetType().Name, GetType().Name, Id));
            }
            peripherals.Add(peripheral);
        }

        public IComponent RemoveComponent(string componentType)
        {
            var currComponent = components.FirstOrDefault(x => x.GetType().Name == componentType);
            if (components.Count == 0 || currComponent == null)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.NotExistingComponent, componentType, GetType().Name, Id));
            }
            components.Remove(currComponent);
            return currComponent;
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            var currPeripheral = peripherals.FirstOrDefault(x => x.GetType().Name == peripheralType);
            if (currPeripheral == null || peripherals.Count == 0)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, GetType().Name, Id));
            }
            peripherals.Remove(currPeripheral);
            return currPeripheral;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($" Components ({components.Count}):");
            foreach (var component in components)
            {
                sb.AppendLine($"  {component}");
            }
            if (peripherals.Any())
            {
                sb.AppendLine($" Peripherals ({peripherals.Count}); Average Overall Performance ({peripherals.Average(x => x.OverallPerformance):F2}):");
                foreach (var peripheral in peripherals)
                {
                    sb.AppendLine($"  {peripheral.ToString()}");
                }
            }
            else
            {
                sb.AppendLine($" Peripherals (0); Average Overall Performance (0.00):");
            }
            return sb.ToString().TrimEnd();
        }
    }
}