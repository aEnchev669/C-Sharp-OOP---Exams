namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        private Database defDb;
        [SetUp]
        public void SetUp()
        {
            defDb = new Database();
        }

        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2, 3, })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void ConstructorShouldInitializeData(int[] data)
        {
            Database db = new Database(data);

            int expectedCount = data.Length;
            int actualCount = db.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 })]
        public void ConstructorShouldThrowExceptionWhenInputdataIsAbove16Symbols(int[] data)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Database db = new Database(data);
            }, "Array's capacity must be exactly 16 integers!");

        }
        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2, 3, })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void ConstructorAddElementsIntoDataField(int[] data)
        {
            Database db = new Database(data);

            int[] expectedData = data;
            int[] actualData = db.Fetch();

            CollectionAssert.AreEqual(expectedData, actualData);
        }
        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2, 3, })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void CountShouldReturnCorrectCount(int[] data)
        {
            Database db = new Database(data);

            int expectedCount = data.Length;
            int actualCount = db.Count;
        }
        [Test]
        public void AddingElementsShouldIncreaseCount()
        {
            int expectedCount = 5;

            for (int i = 1; i <= expectedCount; i++)
            {
                this.defDb.Add(i);
            }

            int actualCount = this.defDb.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddingElementShouldAddThemToTheDataBase()
        {
            int[] expectedData = new int[5];
            for (int i = 1; i <= 5; i++)
            {
                defDb.Add(i);
                expectedData[i - 1] = i;
            }
            int[] actualData = defDb.Fetch();
            CollectionAssert.AreEqual(expectedData, actualData);
        }
        [Test]
        public void AddingMoreThan16ElementsShouldThrowException()
        {
            for (int i = 1; i <= 16; i++)
            {
                defDb.Add(i);
            }
            Assert.Throws<InvalidOperationException>(() =>
            {
                defDb.Add(17);
            }, "Array's capacity must be exactly 16 integers!");
        }
        [Test]
        public void RemovingElementsShouldDecreaseCount()
        {
            int initialCount = 5;
            for (int i = 1; i <= initialCount; i++)
            {
                defDb.Add(i);
            }

            int removeCount = 2;
            for (int i = 1; i <= removeCount; i++)
            {
                defDb.Remove();
            }

            int actualCount = defDb.Count;
            int expectedCOunt = initialCount - removeCount;

            Assert.AreEqual(expectedCOunt, actualCount);
        }
        [Test]
        public void RemoveShouldRemoveItFromDataCollection()
        {
            int initialCount = 5;
            for (int i = 1; i <= initialCount; i++)
            {
                defDb.Add(i);
            }

            int removeCount = 2;
            for (int i = 1; i <= removeCount; i++)
            {
                defDb.Remove();
            }

            int[] expectedData = new[] { 1, 2, 3 };
            int[] actualData = defDb.Fetch();

            CollectionAssert.AreEqual(expectedData,actualData);
        }
        [Test]
        public void RemovingShouldThrowExceptionWHenTheyAreNotElementInDatabase()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                defDb.Remove();
            }, "The collection is empty!");
        }

        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2, 3, })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void Fetch(int[] item)
        {
            Database db = new Database(item);

            int[] expectedData = item;
            int[] actualData = db.Fetch();

            CollectionAssert.AreEqual(expectedData, actualData);
        }
    }
}
