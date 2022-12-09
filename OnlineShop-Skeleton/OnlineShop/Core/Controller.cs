using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineShop.Models.Products.Peripherals;

namespace OnlineShop.Core
{
    public class Controller : IController
    {
        private List<IComputer> computers;
        private List<IComponent> components;
        private List<IPeripheral> peripherals;
        public Controller()
        {
            computers = new List<IComputer>();
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }
        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            var computerCurr = computers.FirstOrDefault(c => c.Id == id);
            if (computerCurr != null)
            {
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);
            }
            IComputer computer = null;
            if (computerType == "DesktopComputer")
            {
                computer = new DesktopComputer(id, manufacturer, model, price);
            }
            else if (computerType == "Laptop")
            {
                computer = new Laptop(id, manufacturer, model, price);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidComputerType);
            }

            computers.Add(computer);
            return string.Format(SuccessMessages.AddedComputer, id);
        }

        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            var computerCurr = computers.FirstOrDefault(c => c.Id == computerId);
            if (computerCurr == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            if (components.FirstOrDefault(t => t.Id == id) != null)
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }
            IComponent component = null;
            if (componentType == "Motherboard")
            {
                component = new Motherboard(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == "PowerSupply")
            {
                component = new PowerSupply(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == "RandomAccessMemory")
            {
                component = new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == "SolidStateDrive")
            {
                component = new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == "VideoCard")
            {
                component = new VideoCard(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == "CentralProcessingUnit")
            {
                component = new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }

            computerCurr.AddComponent(component);
            components.Add(component);
            return String.Format(SuccessMessages.AddedComponent, componentType, id, computerCurr.Id);
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            var computerCurr = computers.FirstOrDefault(c => c.Id == computerId);
            if (computerCurr == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            IPeripheral peripheral = peripherals.FirstOrDefault(p => p.GetType().Name == peripheralType);
            if (peripheral != null)
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }

            if (peripheralType == "Headset")
            {
                peripheral = new Headset(id, manufacturer, model, price, overallPerformance, connectionType);

            }
            else if (peripheralType == "Keyboard")
            {
                peripheral = new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType);

            }
            else if (peripheralType == "Monitor")
            {
                peripheral = new Monitor(id, manufacturer, model, price, overallPerformance, connectionType);

            }
            else if (peripheralType == "Mouse")
            {
                peripheral = new Mouse(id, manufacturer, model, price, overallPerformance, connectionType);

            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPeripheralType);
            }

            computerCurr.AddPeripheral(peripheral);
            peripherals.Add(peripheral);
            return String.Format(SuccessMessages.AddedPeripheral, peripheralType, id, computerId);
        }

        public string BuyBest(decimal budget)
        {
            List<IComputer> computersInBudget = computers.Where(c => c.Price <= budget).ToList();
            if (computers.Count == 0 || computersInBudget.Count == 0)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CanNotBuyComputer, budget));
            }

            computersInBudget.OrderByDescending(t => t.OverallPerformance);
            var computerToBuy = computersInBudget[0];
            computers.Remove(computerToBuy);

            return computerToBuy.ToString();
        }

        public string BuyComputer(int id)
        {
            var computerCurr = computers.FirstOrDefault(c => c.Id == id);
            if (computerCurr == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            computers.Remove(computerCurr);
            return computerCurr.ToString();
        }

        public string GetComputerData(int id)
        {
            var computerCurr = computers.FirstOrDefault(c => c.Id == id);
            if (computerCurr == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            return computerCurr.ToString();
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            var computerCurr = computers.FirstOrDefault(c => c.Id == computerId);
            if (computerCurr == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            IComponent component = components.FirstOrDefault(s => s.GetType().Name == componentType);
            computerCurr.RemoveComponent(componentType);
            components.Remove(component);
            return String.Format(SuccessMessages.RemovedComponent, componentType, component.Id);
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            var computerCurr = computers.FirstOrDefault(c => c.Id == computerId);
            if (computerCurr == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            IPeripheral peripheral = peripherals.FirstOrDefault(p => p.GetType().Name == peripheralType);
            computerCurr.RemovePeripheral(peripheralType);
            peripherals.Remove(peripheral);
            return String.Format(SuccessMessages.RemovedPeripheral, peripheralType, peripheral.Id);
        }

    }
}
