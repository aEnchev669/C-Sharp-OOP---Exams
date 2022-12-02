namespace Book.Tests
{
    using System;

    using NUnit.Framework;

    public class Tests
    {
        [SetUp]
        public void SetUp()
        {

        }
        [Test]
        public void ConstructorShouldInitializeBookNameCorrectly()
        {
            string expectedBookName = "Harry Potter";
            Book book = new Book(expectedBookName, "J. K. Rowling");

            string actualBookName = book.BookName;
            Assert.AreEqual(expectedBookName, actualBookName);
        }
        [Test]
        public void ConstructerShloudInitializeAuthorName()
        {
            string expectedAuthor = "J. K. Rowling";
            Book book = new Book("Harry Potter", expectedAuthor);

            string actualAuthorName = book.BookName;
            Assert.AreEqual(expectedAuthor, actualAuthorName);
        }
    }
}