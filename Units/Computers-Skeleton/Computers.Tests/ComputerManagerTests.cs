using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Computers.Tests
{
    public class Tests
    {
        private ComputerManager computers;
        [SetUp]
        public void Setup()
        {
            computers = new ComputerManager();
        }

        [Test]
        public void TestComputerCtor()
        {
            Computer computer = new Computer("Man", "Acer", 1000);

            Assert.AreEqual("Man", computer.Manufacturer);
            Assert.AreEqual("Acer", computer.Model);
            Assert.AreEqual(1000, computer.Price);
        }
        [Test]
        public void AddComputer()
        {
            Computer computer = new Computer("Man", "Acer", 1000);
            computers.AddComputer(computer);

            Assert.Throws<ArgumentException>(() =>
            {
                computers.AddComputer(computer);

            }, "This computer already exists.");

            Assert.AreEqual(1, computers.Count);
        }
        [Test]
        public void AddComputerhouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                computers.AddComputer(null);
            }, "Can not be null!");
        }
        [Test]
        public void RemoveComputer()
        {
            Computer computer1 = new Computer("Man", "Acer", 1000);
            Computer computer2 = new Computer("Manes", "Asus", 2000);

            computers.AddComputer(computer1);
            computers.AddComputer(computer2);

            var returned = computers.RemoveComputer(computer1.Manufacturer, computer1.Model);

            Assert.AreEqual(1, computers.Count);
            Assert.AreEqual(computer1, returned);
        }
        [Test]
        public void RemoveComputerShouldThrowException()
        {
            Computer computer1 = new Computer("Man", "Acer", 1000);


            computers.AddComputer(computer1);
            Assert.Throws<ArgumentNullException>(() =>
            {
                computers.RemoveComputer("Acer", null);
            }, "Can not be null!");

            Assert.Throws<ArgumentNullException>(() =>
            {
                computers.RemoveComputer(null, "Acer");
            }, "Can not be null!");
        }
        [Test]
        public void GetComputerShouldThrowException()
        {
            Computer computer1 = new Computer("Man", "Acer", 1000);


            computers.AddComputer(computer1);
            Assert.Throws<ArgumentNullException>(() =>
            {
                computers.GetComputer("Acer", null);
            }, "Can not be null!");

            Assert.Throws<ArgumentNullException>(() =>
            {
                computers.GetComputer(null, "Acer");
            }, "Can not be null!");
        }
        [Test]
        public void GetComputer()
        {
            Computer computer1 = new Computer("Man", "Acer", 1000);
            Computer computer2 = new Computer("Manes", "Asus", 2000);

            computers.AddComputer(computer1);
            computers.AddComputer(computer2);

            var actual = computers.GetComputer(computer1.Manufacturer, computer1.Model);

            Assert.AreEqual(computer1, actual);

            Assert.Throws<ArgumentException>(() =>
            {
                computers.GetComputer("GOsho", "Ivan");
            }, "There is no computer with this manufacturer and model.");
        }

        [Test]
        public void GetComputersByManufacturer()
        {
            Computer computer1 = new Computer("Man", "Acer", 1000);
            Computer computer2 = new Computer("Manes", "Asus", 2000);
            Computer computer3 = new Computer("Manes", "Azis", 2000);
            Computer computer4 = new Computer("Manes", "Petur", 2000);

            computers.AddComputer(computer1);
            computers.AddComputer(computer2);
            computers.AddComputer(computer3);
            computers.AddComputer(computer4);

            List<Computer> byManufactures = computers.GetComputersByManufacturer("Manes").ToList();

            Assert.AreEqual(3, byManufactures.Count);
        }
        [Test]
        public void ValidateNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                computers.AddComputer(null);
            }, "Can not be null!");
        }


    }
}