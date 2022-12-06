namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        [TestCase("Seat", "Ibiza", 100.1, 0.3)]
        [TestCase("Seat", "Ibiza", 2, 10)]
        public void ConstructorShouldSetValidData(string make, string model, double capacity, double consumption)
        {
            string expectedMake = make;
            string expectedModel = model;
            double expectedFuelCapacity = capacity;
            double expectedFuelConsumption = consumption;

            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);

            Assert.AreEqual(expectedMake, car.Make);
            Assert.AreEqual(expectedModel, car.Model);
            Assert.AreEqual(expectedFuelConsumption, car.FuelConsumption);
            Assert.AreEqual(expectedFuelCapacity, car.FuelCapacity);
        }
        [TestCase("")]
        [TestCase(null)]
        public void MakeCannotBeNullOrEmpty(string make)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car(make, "Niva", 100, 9.1);
            }, "Make cannot be null or empty!");
        }

        [TestCase("")]
        [TestCase(null)]
        public void ModelCannotBeNullOrEmpty(string model)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car("Lada", model, 100, 9.1);
            }, "Model cannot be null or empty!");
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-1.4)]
        [TestCase(-1900000)]
        public void ConsumptionMustBePossitiveNumber(double consumption)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car("Lada", "Niva", consumption, 9.1);
            }, "Fuel consumption cannot be zero or negative!");
        }
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-1.4)]
        [TestCase(-1900000)]
        public void CapacityMustBePossitiveNumber(double capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car("Lada", "Niva", 100, capacity);
            }, "Fuel capacity cannot be zero or negative!");
        }
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-1.4)]
        [TestCase(-1900000)]
        public void RefuelShouldThrowException(double fuelAmount)
        {
            Car car = new Car("Lada", "Niva", 2.3, 100);

            Assert.Throws<ArgumentException>(() =>
            {
                car.Refuel(fuelAmount);
            }, "Fuel amount cannot be zero or negative!");
        }
        [TestCase(1)]
        [TestCase(99)]
        [TestCase(100)]
        public void RefuelShouldAddGivenFuelToAmount(double fuelToAdd)
        {
            Car car = new Car("Lada", "Niva", 2.3, 100);

            car.Refuel(fuelToAdd);

            Assert.AreEqual(fuelToAdd, car.FuelAmount);
        }
        [TestCase(101)]
        [TestCase(3000)]
        public void RefuelShouldAddGivenFuelToAmountAndIfTheGivenFuelIsMoreThatTheFuelCapacityShouldSetFuelAmountToFuelCapacity(double fuelToAdd)
        {
            Car car = new Car("Lada", "Niva", 2.3, 100);

            car.Refuel(fuelToAdd);

            Assert.AreEqual(100, car.FuelAmount);
        }
        public void DriveShouldDriveGivenKm()
        {
            Car car = new Car("Seat", "Ibiza", 5.5, 60);

            car.Refuel(60);
            double distance = 100;
            double fuelNeeded = (distance / 100) * car.FuelConsumption;
            double expectedFuelAmount = car.FuelAmount - fuelNeeded;

            car.Drive(distance);
            Assert.AreEqual(expectedFuelAmount, car.FuelAmount);

        }
        [Test]
        public void DriveShouldThrowException()
        {
            Car car = new Car("Seat", "Ibiza", 5.5, 60);

            car.Refuel(10);

            double distance = 1000;

            Assert.Throws<InvalidOperationException>(() =>
            {
                car.Drive(distance);
            }, "You don't have enough fuel to drive!");
        }
    }
}