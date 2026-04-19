using Xunit;
using Logic;
using Data.Models;
using System.Linq;

namespace Tests
{
    public class LibraryManagerTests
    {
        // --- TESTING DATA GENERATION METHOD 1 ---
        private Reader GenerateTestReader(string id)
        {
            return new Reader { ReaderId = id, FirstName = "Test", LastName = "User" };
        }

        // --- TESTING DATA GENERATION METHOD 2 ---
        private BookCopy GenerateTestBookCopy(string copyId, bool isAvailable)
        {
            return new BookCopy
            {
                CopyId = copyId,
                IsAvailable = isAvailable,
                BookDetails = new Book { Isbn = "123", Title = "Test Book", Author = "Author" }
            };
        }

        [Fact]
        public void CheckoutBook_ValidData_ReturnsTrueAndRecordsEvent()
        {
            // Arrange - Setup fake database and generated data
            var fakeRepo = new FakeDataRepository();
            var reader = GenerateTestReader("R1");
            var copy = GenerateTestBookCopy("C1", true); // Book is available

            fakeRepo.AddReader(reader);
            fakeRepo.AddBookCopy(copy);

            var manager = new LibraryManager(fakeRepo);

            // Act
            var result = manager.CheckoutBook("R1", "C1");

            // Assert
            Assert.True(result);
            Assert.False(copy.IsAvailable); // Verify state changed
            Assert.Single(fakeRepo.Events); // Verify event was recorded
            Assert.Equal("Checkout", fakeRepo.Events.First().ActionType);
        }

        [Fact]
        public void CheckoutBook_BookNotAvailable_ReturnsFalse()
        {
            // Arrange
            var fakeRepo = new FakeDataRepository();
            var reader = GenerateTestReader("R1");
            var copy = GenerateTestBookCopy("C1", false); // Book is NOT available

            fakeRepo.AddReader(reader);
            fakeRepo.AddBookCopy(copy);

            var manager = new LibraryManager(fakeRepo);

            // Act
            var result = manager.CheckoutBook("R1", "C1");

            // Assert
            Assert.False(result);
            Assert.Empty(fakeRepo.Events); // No checkout event should be recorded
        }

        [Fact]
        public void ReturnBook_ValidData_ReturnsTrueAndRecordsEvent()
        {
            // Arrange
            var fakeRepo = new FakeDataRepository();
            var copy = GenerateTestBookCopy("C1", false); // Book is currently checked out

            fakeRepo.AddBookCopy(copy);

            var manager = new LibraryManager(fakeRepo);

            // Act
            var result = manager.ReturnBook("C1");

            // Assert
            Assert.True(result);
            Assert.True(copy.IsAvailable); // Verify state is restored
            Assert.Single(fakeRepo.Events);
            Assert.Equal("Return", fakeRepo.Events.First().ActionType);
        }
    }
}