namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;

    public class AquariumsTests
    {
        [Test]
        public void Fish()
        {
            Fish fish = new Fish("GOsho");

            Assert.AreEqual("GOsho", fish.Name);
            Assert.IsTrue(fish.Available);
        }
        [Test]
        public void AquariumCtor()
        {
            Fish fish = new Fish("GOsho");

            Aquarium aquarium = new Aquarium("Nasa", 10);

            Assert.AreEqual("Nasa", aquarium.Name);
            Assert.AreEqual(10, aquarium.Capacity);
        }
        [TestCase("")]
        [TestCase(null)]
        public void NameShouldThrowException(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Aquarium aquarium = new Aquarium(name, 10);
            }, "Invalid aquarium name!");
        }
        [TestCase(-1)]
        [TestCase(-1000)]
        public void CapacityShouldThrowException(int capacity)
        {
             Assert.Throws<ArgumentException>(() =>
            {
                Aquarium aquarium = new Aquarium("Gosho", capacity);
            }, "Invalid aquarium capacity!");
        }
        [Test]
        public void AddFish()
        {
            Fish fish = new Fish("GOsho");
            Aquarium aquarium = new Aquarium("Gosho", 0);

            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.Add(fish);
            }, "Aquarium is full!");
            Fish fish2 = new Fish("GOsho1");
            Fish fish3 = new Fish("GOsho2");
            Fish fish4 = new Fish("GOsho3");
           
            aquarium = new Aquarium("Pesho", 10);
            aquarium.Add(fish);
            aquarium.Add(fish2);
            aquarium.Add(fish3);
            aquarium.Add(fish4);

            Assert.AreEqual(4, aquarium.Count);
        }
        [Test]
        public void RemoveFIsh()
        {
            Fish fish = new Fish("GOsho");
            Fish fish2 = new Fish("GOsho1");

            Aquarium aquarium = new Aquarium("Gosho", 12);

            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.RemoveFish(fish.Name);
            }, $"Fish with the name {fish.Name} doesn't exist!");

            aquarium.Add(fish);
            aquarium.Add(fish2);

            aquarium.RemoveFish(fish.Name);

            Assert.AreEqual(1, aquarium.Count);
        }
        [Test]
        public void SelfFish()
        {
            Fish fish = new Fish("GOsho");
            Fish fish2 = new Fish("GOsho1");

            Aquarium aquarium = new Aquarium("Gosho", 12);

            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.SellFish(fish.Name);
            }, $"Fish with the name {fish.Name} doesn't exist!");

            aquarium.Add(fish);
            aquarium.Add(fish2);

           var fishAv = aquarium.SellFish(fish.Name);

            Assert.IsFalse(fish.Available);

            Assert.AreEqual(fish, fishAv);
        }
        [Test]
        public void Report()
        {
            Aquarium aquarium = new Aquarium("Gosho", 12);

            Fish fish = new Fish("GOsho");
            Fish fish2 = new Fish("GOsho1");

            aquarium.Add(fish);
            aquarium.Add(fish2);
            string expected = $"Fish available at {aquarium.Name}: GOsho, GOsho1";
            string actual = aquarium.Report();

            Assert.AreEqual(expected, actual);
        }

    }
}
