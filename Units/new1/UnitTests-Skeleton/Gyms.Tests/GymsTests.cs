using NUnit.Framework;
using System;

namespace Gyms.Tests
{
    public class GymsTests
    {
        [Test]
        public void AthleteCtor()
        {
            Athlete athlete = new Athlete("Gosho");

            Assert.AreEqual("Gosho", athlete.FullName);
        }
        [Test]
        public void GymCtor()
        {
            Gym gym = new Gym("Gym", 10);

            Assert.AreEqual("Gym", gym.Name);
            Assert.AreEqual(10, gym.Capacity);
        }
        [TestCase("")]
        [TestCase(null)]
        public void TestGymExceptionName(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Gym gym = new Gym(name, 10);
            }, "Invalid gym name.");
        }
        [TestCase(-1)]
        [TestCase(-1111)]
        public void TestGymExceptionCapacity(int size)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Gym gym = new Gym("Ivan", size);
            }, "Invalid gym capacity.");
        }
        [Test]
        public void AddAthlete()
        {
            Gym gym = new Gym("Ivan", 1);
            Athlete athlete1 = new Athlete("Gosho");
            Athlete athlete2 = new Athlete("Petur");
            gym.AddAthlete(athlete1);
            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.AddAthlete(athlete2);
            }, "The gym is full.");

            gym = new Gym("GYMEE", 2);
            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);

            Assert.AreEqual(2, gym.Count);
        }
        [Test]
        public void GymRemove()
        {
            string fullName = "Gosho";
            Gym gym = new Gym("YE", 2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.RemoveAthlete(fullName);
            }, $"The athlete {fullName} doesn't exist.");

            Athlete athlete1 = new Athlete("Gosho");
            Athlete athlete2 = new Athlete("Ivan");

            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete1);

            gym.RemoveAthlete(fullName);

            Assert.AreEqual(1, gym.Count);
        }
        [Test]
        public void GymInjureAthlete()
        {
            string fullName = "Gosho";
            Gym gym = new Gym("YE", 2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.InjureAthlete(fullName);
            }, $"The athlete {fullName} doesn't exist.");

            Athlete athlete1 = new Athlete("Gosho");

            gym.AddAthlete(athlete1);

            gym.InjureAthlete(fullName);

            Assert.IsTrue(athlete1.IsInjured);
        }
        [Test]
        public void Report()
        {
            Athlete athlete1 = new Athlete("Gosho");
            Gym gym = new Gym("YE", 2);

            gym.AddAthlete(athlete1);

            string expected = $"Active athletes at {gym.Name}: {athlete1.FullName}";
            string actual = gym.Report();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Athelts()
        {
            Athlete athlete1 = new Athlete("Gosho");
            Athlete athlete2 = new Athlete("Ivan");

            Gym gym = new Gym("YE", 2);

            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete1);

            int count = gym.Count;

            Assert.AreEqual(2, count);
        }

    }
}
