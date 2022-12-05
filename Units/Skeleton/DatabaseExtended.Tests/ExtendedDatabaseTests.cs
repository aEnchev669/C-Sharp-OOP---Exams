namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database defDb;
        [SetUp]
        public void SetUp()
        {
            Person[] persons = new Person[15];

            for (int i = 0; i < 15; i++)
            {
                int id = i;
                string userName = ((char)('N' + i)).ToString();
                Person person = new Person(id, userName);
                persons[i] = person;
            }
            defDb = new Database(persons);
        }

        [Test]
        public void ConstructorPersonTest()
        {
            Person person = new Person(123, "Ivan");
            Person[] prsonsArray = new Person[] { person };

            Database db = new Database(prsonsArray);

            int expectedCount = prsonsArray.Length;
            int actualCount = db.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
        [Test]
        public void AddRangeShouldThrowExceptionIfArrayIs16OrMore()
        {
            Person[] persons = new Person[17];

            for (int i = 0; i <= 16; i++)
            {
                int id = i;
                string userName = ((char)('N' + i)).ToString();
                Person person = new Person(id, userName);
                persons[i] = person;
            }
            Assert.Throws<ArgumentException>(() =>
            {
                Database db = new Database(persons);
            }, "Provided data length should be in range [0..16]!");
        }

        [Test]
        public void AddShouldNotAddAbove16()
        {
            defDb.Add(new Person(222, "Gosho"));

            Assert.Throws<InvalidOperationException>(() =>
            {
                defDb.Add(new Person(333, "Ivan"));
            }, "Array's capacity must be exactly 16 integers!");
        }
        [Test]
        public void AddShouldThrowexceptionIfWeTryToAdd2PersonsWithSameNames()
        {
            Person[] person = new Person[] { new Person(666, "Petur") };
            Database db = new Database(person);

            Assert.Throws<InvalidOperationException>(() =>
            {
                db.Add(new Person(1333, "Petur"));
            }, "There is already user with this username!");
        }
        [Test]
        public void AddShouldThrowexceptionIfWeTryToAdd2PersonsWithSameIDs()
        {
            Person[] person = new Person[] { new Person(999, "Gosho") };
            Database db = new Database(person);

            Assert.Throws<InvalidOperationException>(() =>
            {
                db.Add(new Person(999, "Petur"));
            }, "There is already user with this Id!");
        }

        [Test]
        public void AddMethodCounter()
        {
            Person person = new Person(122, "Gosho");

            Database db = new Database();
            db.Add(person);

            int expectedCount = 1;
            int actualCount = db.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
        [Test]
        public void AddMustAddToTheCollection()
        {
            Person person = new Person(123, "Gosho");
            Database db = new Database();

            db.Add(person);

            int expectedCount = 1;
            int actualCount = db.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
        [Test]
        public void RemoveMustRemoveItemFromCOllection()
        {
            int expectedCount = defDb.Count - 1;

            defDb.Remove();
            Assert.AreEqual(expectedCount, defDb.Count);
        }
        [Test]
        public void RemoveMustThrowExceptionIfCountIsZero()
        {
            Database db = new Database();

            Assert.Throws<InvalidOperationException>(() =>
            {
                db.Remove();
            });
        }

        [Test]
        public void RemoveMustSetPositionCountNull()
        {
            Person[] persons = new Person[15];

            for (int i = 0; i < 15; i++)
            {
                string userName = ('N' + i).ToString(); //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                Person person = new Person(i, userName);
                persons[i] = person;
            }
            defDb = new Database(persons);

            defDb.Remove();

            Assert.AreEqual(null, persons[defDb.Count] = null);
        }
        [TestCase("")]
        [TestCase(null)]
        public void FindByUserNameMustThrowExceptionIfUsernameIsNullOrEmpty(string person)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                defDb.FindByUsername(person);
            }, "Username parameter is null!");
        }
        [Test]
        public void FindByUsernameMustThrowExceptionIfUserDoesNotExists()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                defDb.FindByUsername("Dimitrichko");
            }, "No user is present by this username!");
        }
        [Test]
        public void FindByUsernameMustReturnTheUserWithTheGivenName()
        {
            Person expectedPerson = new Person(12, "Petko");
            Database db = new Database(expectedPerson);

            Person actualPerson = db.FindByUsername("Petko");

            Assert.AreEqual(expectedPerson, actualPerson);
        }
        [Test]
        public void FindByIdMustThrowExceptionIfTheGivenIdIsNegativeOrZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                defDb.FindById(-23);
            }, "Id should be a positive number!");
        }
        [Test]
        public void FindByIdMustThrowExceptionIfPersonWithTheGivenIdDoesNotExist()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                defDb.FindById(123);
            }, "No user is present by this ID!");
        }
        [Test]
        public void FindByIdMustReturtTheRightPerson()
        {
            Person expectedPerson = new Person(123, "Gosho");

            Database db = new Database(expectedPerson);

            Person actualPerson = db.FindById(123);

            Assert.AreEqual(expectedPerson, actualPerson);
        }
    }
}
