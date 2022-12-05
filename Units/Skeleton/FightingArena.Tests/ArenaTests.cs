namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena; 
        [SetUp]
        public void SetUp()
        {
            arena = new Arena();
        }

        [Test]
        public void ConstructorShouldInitializeWarriorCollection()
        {
            Arena ctorArena = new Arena();

            Assert.IsNotNull(ctorArena.Warriors);
        }
        [Test]
        public void EnrollShouldAddWarriorToTheCollection()
        {
           
            Warrior warrior = new Warrior("Gosho", 20, 50);

            arena.Enroll(warrior);

            bool isWarriorEnrolled = arena.Warriors.Contains(warrior);

            Assert.IsTrue(isWarriorEnrolled);
        }
        [Test]
        public void EnrollShouldThrowExceptionIfTheGivenWarriorIsAlreadyEnrolledForTheFight()
        {
            Warrior warrior = new Warrior("GOsho", 20, 50);

            arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Enroll(warrior);
            }, "Warrior is already enrolled for the fights!");
        }
        [Test]
        public void EnrollShouldThrowExceptionIfTheGivenNameAlreadyExist()
        {
            Warrior warrior1 = new Warrior("GOsho", 20, 50);
            Warrior warrior2 = new Warrior("GOsho", 15, 60);

            arena.Enroll(warrior1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Enroll(warrior2);
            }, "Warrior is already enrolled for the fights!");
        }

        [Test]
        public void CountShouldReturnEnrolledWarriorsCount()
        {
            Warrior warrior1 = new Warrior("Peshp", 20, 50);
            Warrior warrior2 = new Warrior("G0sho", 15, 60);

            arena.Enroll(warrior2);
            arena.Enroll(warrior1);

            int expectedCOunt = 2;
            int actualCOunt = arena.Count;

            Assert.AreEqual(expectedCOunt, actualCOunt);
        }
        [Test]
        public void CountShouldReturnZeroWhenNoWarriorsAreEnrolled()
        {
            int expectedCOunt = 0;
            int actualCOunt = arena.Count;
            Assert.AreEqual(expectedCOunt, actualCOunt);
        }
        [Test]
        public void FightShouldThrowExceptionWithNonExistingDeffender()
        {
            Warrior warrior1 = new Warrior("Peshp", 20, 50);
            Warrior warrior2 = new Warrior("G0sho", 15, 60);

            arena.Enroll(warrior1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Fight(warrior1.Name, warrior2.Name);
            }, $"There is no fighter with name {warrior2} enrolled for the fights!");
        }
        [Test]
        public void FightShouldThrowExceptionWithNonExistingAttacker()
        {
            Warrior warrior1 = new Warrior("Peshp", 20, 50);
            Warrior warrior2 = new Warrior("G0sho", 15, 60);

            arena.Enroll(warrior2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Fight(warrior1.Name, warrior2.Name);
            }, $"There is no fighter with name {warrior1} enrolled for the fights!");
        }
        [Test]
        public void FightShouldThrowExceptionWithNonExistingWarriors()
        {
            Warrior warrior1 = new Warrior("Peshp", 20, 50);
            Warrior warrior2 = new Warrior("G0sho", 15, 60);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Fight(warrior1.Name, warrior2.Name);
            }, $"There is no fighter with name {warrior2} enrolled for the fights!");
        }
        [Test]
        public void FightShouldSecceedWhenWarriorsExist()
        {
            Warrior warrior1 = new Warrior("Peshp", 20, 100);
            Warrior warrior2 = new Warrior("G0sho", 10, 100);

            arena.Enroll(warrior2);
            arena.Enroll(warrior1);

            arena.Fight(warrior1.Name, warrior2.Name);


            int expectedHpW1 = 90;
            int expectedHpW2 = 80;

            int actualW1Hp = arena.Warriors.First(n => n.Name == warrior1.Name).HP;
            int actualW2Hp = arena.Warriors.First(n => n.Name == warrior2.Name).HP;

            Assert.AreEqual(expectedHpW1, actualW1Hp);
            Assert.AreEqual(expectedHpW2, actualW2Hp);
        }
    }
}
