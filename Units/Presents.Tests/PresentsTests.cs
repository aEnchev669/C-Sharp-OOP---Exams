namespace Presents.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class PresentsTests
    {
        private Bag bag;
        [SetUp]
        public void SetUp()
        {
            bag = new Bag();
        }
        [Test]
        public void TestPresentCtorShouldRetunCurrValue()
        {
            Present present = new Present("Gosho", 23);

            Assert.AreEqual("Gosho", present.Name);
            Assert.AreEqual(23, present.Magic);
        }

        [Test]
        public void TestBagCreate()
        {
            Present present = new Present("Gosho", 23);
            Assert.Throws<ArgumentNullException>(() =>
            {
                bag.Create(null);
            }, "Present is null");

            bag.Create(present);

            Assert.Throws<InvalidOperationException>(() =>
            {
                bag.Create(present);
            }, "This present already exists!");


            bag = new Bag();
            string text = bag.Create(present);
            Assert.AreEqual($"Successfully added present {present.Name}.", text);

            Assert.AreEqual(1, bag.GetPresents().Count);
        }
        [Test]
        public void TestRemove()
        {
            Present present = new Present("Gosho", 23);
            bag.Create(present);

            bag.Remove(present);

            Assert.AreEqual(0, bag.GetPresents().Count);
        }
        [Test]
        public void TestGetPresentWithLeastMagic()
        {
            Present present1 = new Present("Petur", 44);
            Present present2 = new Present("Gosho", 23);

            bag.Create(present2);
            bag.Create(present1);

            Present actual = bag.GetPresentWithLeastMagic();

            Assert.AreEqual(present2, actual);
        }
        [Test]
        public void TestGetPresent()
        {
            Present present1 = new Present("Petur", 44);
            Present present2 = new Present("Gosho", 13);
            Present present3 = new Present("Ivan", 26);
            Present present4 = new Present("Pesho", 23);

            bag.Create(present1);
            bag.Create(present2);
            bag.Create(present3);
            bag.Create(present4);

            Present actural = bag.GetPresent("Gosho");
            Assert.AreEqual(present2, actural);
        }

    }
}