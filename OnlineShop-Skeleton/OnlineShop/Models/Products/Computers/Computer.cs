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
        public Computer(int id, string manufacturer, string model, decimal price, double overallPerformance) : base(id, manufacturer, model, price, overallPerformance)
        {
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }

        private List<IComponent> components;
        private List<IPeripheral> peripherals;
        public IReadOnlyCollection<IComponent> Components => components;

        public IReadOnlyCollection<IPeripheral> Peripherals => peripherals;

        public override double OverallPerformance => components.Any() ? components.Sum(c => c.OverallPerformance) : GetType().Name == "DesktopComputer" ? 15 : 10; //!!!!!!!!!!!!!!!!!!!!!!1

        public override decimal Price => base.Price + components.Sum(p => p.Price) + peripherals.Sum(p => p.Price);
        public void AddComponent(IComponent component)
        {
            if (components.Any(c => c.GetType().Name == component.GetType().Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingComponent, component.GetType().Name, GetType().Name, Id));
            }
            components.Add(component);
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            if (peripherals.Any(c => c.GetType().Name == peripheral.GetType().Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingPeripheral, peripheral.GetType().Name, GetType().Name, Id));
            }
            peripherals.Add(peripheral);
        }

        public IComponent RemoveComponent(string componentType)
        {
            IComponent component = components.FirstOrDefault(c => c.GetType().Name == componentType);
            if (component == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingComponent, componentType, GetType().Name, Id));
            }

            components.Remove(component);
            return component;
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            IPeripheral peripheral = peripherals.FirstOrDefault(c => c.GetType().Name == peripheralType);
            if (peripheral == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, GetType().Name, Id));
            }
            
            peripherals.Remove(peripheral);
            return peripheral;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($" Components ({components.Count})");
            foreach (var component in components)
            {
                sb.AppendLine($"  {component}");
            }
            if (peripherals.Any())
            {
                sb.AppendLine($" Peripherals ({peripherals.Count}); Average Overall Performance ({peripherals.Average(t => t.OverallPerformance)}:f2):");
                foreach (var peripheral in peripherals)
                {
                    sb.AppendLine($"  {peripheral}");
                }
            }
            else
            {
                sb.AppendLine($" Peripherals ({peripherals.Count}); Average Overall Performance (0.00):");
            }

            

            return sb.ToString().TrimEnd();

        }
    }
}
