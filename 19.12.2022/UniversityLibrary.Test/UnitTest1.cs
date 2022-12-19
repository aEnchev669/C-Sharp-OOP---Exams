namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Text;

    public class Tests
    {
        private List<TextBook> textBooks;
        [SetUp]
        public void Setup()
        {

            textBooks = new List<TextBook>();


        }

        [Test]
        public void AddTextBookToLibrary()
        {
            TextBook textBook = new TextBook("Brozne", "J.K", "AAA");

            UniversityLibrary universityLibrary = new UniversityLibrary();
            universityLibrary.AddTextBookToLibrary(textBook);

            int count = universityLibrary.Catalogue.Count;

            Assert.AreEqual(1, count);
        }

        [Test]
        public void LoanTextBookRetWrong()
        {
            TextBook textBook = new TextBook("Bronze", "J.K", "AAA");
            textBook.InventoryNumber = 1;
            UniversityLibrary universityLibrary = new UniversityLibrary();
            universityLibrary.AddTextBookToLibrary(textBook);
            universityLibrary.LoanTextBook(1, "Gosho");

            string ac = universityLibrary.LoanTextBook(1, "Gosho");

            string ex = $"Gosho still hasn't returned Bronze!";

            Assert.AreEqual(ex, ac);
        }
        [Test]
        public void ReturnTextBook()
        {
            TextBook textBook = new TextBook("Bronze", "J.K", "AAA");
            textBook.InventoryNumber = 1;
            UniversityLibrary universityLibrary = new UniversityLibrary();

            universityLibrary.AddTextBookToLibrary(textBook);

            string ac = universityLibrary.ReturnTextBook(1);

            string ex = $"Bronze is returned to the library.";

            Assert.AreEqual(ex, ac);
        }
        [Test]
        public void LoanTextBookRet()
        {
            TextBook textBook = new TextBook("Bronze", "J.K", "AAA");
            textBook.InventoryNumber = 1;
            UniversityLibrary universityLibrary = new UniversityLibrary();
            universityLibrary.AddTextBookToLibrary(textBook);
          

            string ac = universityLibrary.LoanTextBook(1, "Ivan");

            string ex = $"{textBook.Title} loaned to Ivan.";

            Assert.AreEqual(ex, ac);
        }
        [Test]
        public void set()
        {
            TextBook textBook = new TextBook("Bronze", "J.K", "AAA");
            textBook.InventoryNumber = 1;
            UniversityLibrary universityLibrary = new UniversityLibrary();
            universityLibrary.AddTextBookToLibrary(textBook);

            string ac = universityLibrary.LoanTextBook(1, "Ivan");

            Assert.AreEqual("Ivan", textBook.Holder);
        }
        [Test]
        public void ret()
        {
            TextBook textBook = new TextBook("Bronze", "J.K", "AAA");
            textBook.InventoryNumber = 1;
            UniversityLibrary universityLibrary = new UniversityLibrary();

            universityLibrary.AddTextBookToLibrary(textBook);

           universityLibrary.ReturnTextBook(1);

            

            Assert.AreEqual("", textBook.Holder);
        }
        [Test]
        public void Count()
        {
            TextBook textBook1 = new TextBook("Bronze", "J.K", "AAA");
            TextBook textBook2 = new TextBook("Gold", "J.K", "bbb");

            UniversityLibrary universityLibrary = new UniversityLibrary();
            universityLibrary.AddTextBookToLibrary(textBook1);
            universityLibrary.AddTextBookToLibrary(textBook2);

            Assert.AreEqual(2, universityLibrary.Catalogue.Count);
        }
        [Test]
        public void ctor()
        {
            TextBook textBook1 = new TextBook("Bronze", "J.K", "AAA");
            

            UniversityLibrary universityLibrary = new UniversityLibrary();
            universityLibrary.AddTextBookToLibrary(textBook1);

            TextBook book = universityLibrary.Catalogue.Find(t => t.Title == "Bronze");
            Assert.AreEqual(textBook1, book);

        }
        [Test]
        public void Add()
        {
            TextBook textBook1 = new TextBook("Bronze", "J.K", "AAA");

            UniversityLibrary universityLibrary = new UniversityLibrary();
            universityLibrary.AddTextBookToLibrary(textBook1);

           
            Assert.AreEqual(1, universityLibrary.Catalogue.Count);
        }
        [Test]
        public void invNumber()
        {
            TextBook textBook = new TextBook("Brozne", "J.K", "AAA");

            UniversityLibrary universityLibrary = new UniversityLibrary();
            universityLibrary.AddTextBookToLibrary(textBook);

            Assert.AreEqual(1, textBook.InventoryNumber);
        }
        [Test]
        public void addReturn()
        {

            TextBook textBook = new TextBook("Bronze", "J.K", "AAA");

            UniversityLibrary universityLibrary = new UniversityLibrary();
            string actual = universityLibrary.AddTextBookToLibrary(textBook);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Book: {textBook.Title} - {textBook.InventoryNumber}");
            sb.AppendLine($"Category: {textBook.Category}");
            sb.AppendLine($"Author: {textBook.Author}");

          string expected = sb.ToString().TrimEnd();
            Assert.AreEqual(expected, actual);
        }
    }
}