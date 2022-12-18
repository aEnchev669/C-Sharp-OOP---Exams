using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
            [Test]
            public void CarCtor()
            {
                Car car = new Car("Lada", 12);

                Assert.AreEqual("Lada", car.CarModel);
                Assert.AreEqual(12, car.NumberOfIssues);
            }
            [Test]
            public void GarageCtor()
            {
                Garage garage = new Garage("Gar", 6);

                Assert.AreEqual("Gar", garage.Name);
                Assert.AreEqual(6, garage.MechanicsAvailable);
            }
            [Test]
            public void GarageName()
            {
                Garage garage = new Garage("Gar", 6);

                Assert.AreEqual("Gar", garage.Name);


            }
            [TestCase("")]
            [TestCase(null)]
            public void GarageNameException(string name)
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    Garage garage = new Garage(name, 6);
                });
            }
            [TestCase(0)]
            [TestCase(-1)]
            [TestCase(-100)]
            public void GarageMehaException(int meh)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    Garage garage = new Garage("Gar", meh);
                });
            }
            [Test]
            public void CarAddException()
            {
                Car car = new Car("Lada", 12);
                Car car1 = new Car("Kola", 12);
                Garage garage = new Garage("Gar", 1);
                garage.AddCar(car);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.AddCar(car1);
                });
            }
            [Test]
            public void CarAdd()
            {
                Car car = new Car("Lada", 12);
                Car car1 = new Car("Kola", 12);
                Garage garage = new Garage("Gar", 3);
                garage.AddCar(car);
                garage.AddCar(car1);

                Assert.AreEqual(2, garage.CarsInGarage);
            }
            [Test]
            public void FixCarException()
            {

                Garage garage = new Garage("Gar", 1);


                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.FixCar("Lada");
                });
            }
            [Test]
            public void FixCar()
            {
                Car car = new Car("Lada", 12);

                Garage garage = new Garage("Gar", 3);
                garage.AddCar(car);

                garage.FixCar("Lada");

                Assert.AreEqual(0, car.NumberOfIssues);
            }
            [Test]
            public void FixCarReturn()
            {
                Car car = new Car("Lada", 12);

                Garage garage = new Garage("Gar", 3);
                garage.AddCar(car);

                Car car1 = garage.FixCar("Lada");

                Assert.AreEqual(car, car1);
            }
            [Test]
            public void RemoveFixedCarsExc()
            {

                Garage garage = new Garage("Gar", 1);


                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.RemoveFixedCar();
                });
            }
            [Test]
            public void RemoveFixedCars()
            {

                Garage garage = new Garage("Gar", 3);
                Car car = new Car("Lada", 12);
                Car car1 = new Car("Kola", 12);
                
                garage.AddCar(car);
                garage.AddCar(car1);

                garage.FixCar("Kola");
                garage.FixCar("Lada");

              int cars =  garage.RemoveFixedCar();

                Assert.AreEqual(2, cars);
            }
            
            [Test]
            public void ReportCars()
            {

                Garage garage = new Garage("Gar", 3);
                Car car = new Car("Lada", 12);
              

                garage.AddCar(car);


                string output = garage.Report();
                string expected = $"There are {1} which are not fixed: {car.CarModel}.";
                Assert.AreEqual(expected, output);
            }
        }
    }
}