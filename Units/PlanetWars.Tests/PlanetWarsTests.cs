using NUnit.Framework;
using System;
using System.Linq;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            Planet planet;
            [SetUp]
            public void SetUp()
            {
                planet = new Planet("Pluton", 1000);
            }
            [Test]
            public void WeaponCtor()
            {
                Weapon weapon = new Weapon("Gosho", 15, 10);

                string exName = "Gosho";
                int exPrice = 15;
                int exLvl = 10;

                Assert.AreEqual(exName, weapon.Name);
                Assert.AreEqual(exPrice, weapon.Price);
                Assert.AreEqual(exLvl, weapon.DestructionLevel);
            }
            [TestCase(-1)]
            [TestCase(-1000)]
            [TestCase(-1000.2)]
            public void PriceShouldThrowException(double price)
            {

                Assert.Throws<ArgumentException>(() =>
                {
                    Weapon weapon = new Weapon("Gosho", price, 10);
                }, "Price cannot be negative.");
            }
            [Test]
            public void IncreaseDistructionLvlWepaon()
            {
                Weapon weapon = new Weapon("Gosho", 15, 10);

                weapon.IncreaseDestructionLevel();

                Assert.AreEqual(11, 11);
            }
            [TestCase(10)]
            [TestCase(1000)]
            public void IsNuclearWeapon(int desLvl)
            {
                Weapon weapon = new Weapon("Gosho", 15, desLvl);

                Assert.IsTrue(weapon.IsNuclear);
            }
            [TestCase(9)]
            [TestCase(-19)]
            [TestCase(0)]
            public void IsNuclearWeaponIsFalse(int desLvl)
            {
                Weapon weapon = new Weapon("Gosho", 15, desLvl);

                Assert.IsFalse(weapon.IsNuclear);
            }

            [Test]
            public void PlanetConstructor()
            {
                Planet planetCtor = new Planet("Earth", 1002);

                Assert.AreEqual("Earth", planetCtor.Name);
                Assert.AreEqual(1002, planetCtor.Budget);
            }
            [TestCase("")]
            [TestCase(null)]
            public void InvalidPlanetName(string name)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    Planet planetName = new Planet(name, 100);
                }, "Invalid planet Name");
            }
            [TestCase(-1)]
            [TestCase(-1000)]
            [TestCase(-1000.1)]
            public void InvalidPlanetName(double budget)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    Planet planetName = new Planet("Gosho", budget);
                }, "Budget cannot drop below Zero!");
            }
            [Test]
            public void MilitaryPowerRatioShoudlWork()
            {

                Weapon weapon1 = new Weapon("Petur", 12, 21);
                Weapon weapon2 = new Weapon("Ivan", 12, 29);

                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);

                double expected = 50;
                double actural = planet.MilitaryPowerRatio;

                Assert.AreEqual(expected, actural);
            }
            [TestCase(-2)]
            [TestCase(0)]
            [TestCase(16)]
            [TestCase(16.2)]
            public void PlanetProfit(double amount)
            {
                double expectedBudget = amount + planet.Budget;

                planet.Profit(amount);

                double actualBudget = planet.Budget;

                Assert.AreEqual(expectedBudget, actualBudget);
            }
            [TestCase(1000)]
            [TestCase(1)]
            [TestCase(0)]
            [TestCase(-1)]
            [TestCase(21.2)]
            public void SpendFundsShouldIncreaseBudgetWithGuvenAMount(double amount)
            {
                double expected = planet.Budget - amount;
                planet.SpendFunds(amount);
                double actual = planet.Budget;

                Assert.AreEqual(expected, actual);
            }
            [TestCase(1001)]
            [TestCase(10000)]
            public void SpendFundsShouldThrowException(double amount)
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.SpendFunds(amount);
                }, $"Not enough funds to finalize the deal.");
            }
            [Test]
            public void AddWeaponShouldThrowException()
            {
                Weapon weapon1 = new Weapon("Petur", 12, 21);
                Weapon weapon2 = new Weapon("Petur", 12, 29);

                planet.AddWeapon(weapon1);
                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.AddWeapon(weapon2);
                }, $"There is already a {weapon1.Name} weapon.");
            }
            [Test]
            public void AddWeaponShouldAddWeapon()
            {
                Weapon weapon1 = new Weapon("Petur", 12, 21);
                Weapon weapon2 = new Weapon("Gosho", 12, 29);

                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);

                Assert.AreEqual(2, planet.Weapons.Count);
            }
            [Test]
            public void RemoveShouldRemoveWeaponFromTheCollection()
            {
                Weapon weapon1 = new Weapon("Petur", 12, 21);
                Weapon weapon2 = new Weapon("Gosho", 12, 29);

                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);

                planet.RemoveWeapon("Petur");
                Assert.AreEqual(1, planet.Weapons.Count);
            }
            [Test]
            public void RemoveShouldtRemoveWeaponFromTheCollection()
            {
                Weapon weapon1 = new Weapon("Petur", 12, 21);
                Weapon weapon2 = new Weapon("Gosho", 12, 29);

                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);

                planet.RemoveWeapon("Ivan");
                Assert.AreEqual(2, planet.Weapons.Count);
            }
            [Test]
            public void UpgradeWeaponShouldThrowExcpetionIftheGivenWeaponDoesNotExist()
            {
                string weaponName = "PEsho";
                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.UpgradeWeapon(weaponName);
                }, $"{weaponName} does not exist in the weapon repository of {planet.Name}");
            }
            [Test]
            public void UpgradeWeaponShouldUpgradeTheGivenWeapon()
            {
                Weapon weapon1 = new Weapon("Petur", 12, 21);
                string name = weapon1.Name;
                planet.AddWeapon(weapon1);
                planet.UpgradeWeapon(weapon1.Name);

                double expected = 22;
                double actual = planet.Weapons.First(n => n.Name == name).DestructionLevel;

                Assert.AreEqual(expected, actual);
            }
            [Test]
            public void DestructOpponentShouldThrowException()
            {
                Weapon weapon1 = new Weapon("Petur", 12, 20);
                Weapon weapon2 = new Weapon("Petur", 12, 21);

                Planet attacker = new Planet("Earth", 1000);
                Planet deffender = new Planet("Earth", 1000);

                attacker.AddWeapon(weapon1);
                deffender.AddWeapon(weapon2);
                Assert.Throws<InvalidOperationException>(() =>
                {
                    attacker.DestructOpponent(deffender);
                }, $"{deffender.Name} is too strong to declare war to!");
            }
            [Test]
            public void DestructOpponentShouldPrint()
            {
                Weapon weapon1 = new Weapon("Petur", 12, 22);
                Weapon weapon2 = new Weapon("Petur", 12, 21);

                Planet attacker = new Planet("Earth", 1000);
                Planet deffender = new Planet("Earth", 1000);

                attacker.AddWeapon(weapon1);
                deffender.AddWeapon(weapon2);

                string exMessage = $"{deffender.Name} is destructed!";
                string acMessage = attacker.DestructOpponent(deffender);

                Assert.AreEqual(exMessage, acMessage);

            }
        }

    }
}
