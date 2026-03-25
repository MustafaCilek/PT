using Xunit;
using Logic;
using Data;

namespace Tests
{
    public class LibraryManagerTests
    {
        [Fact]
        public void AddBook_IncreasesAvailableCount()
        {
            // Arrange
            var manager = new LibraryManager();
            var newBook = new Book { Title = "C# in Depth", Author = "Jon Skeet" };

            // Act
            manager.AddBook(newBook);

            // Assert
            Assert.Equal(1, manager.GetAvailableBookCount());
        }

        [Fact]
        public void CheckoutBook_WhenAvailable_ReturnsTrueAndDecreasesCount()
        {
            // Arrange
            var manager = new LibraryManager();
            manager.AddBook(new Book { Title = "Clean Code" });

            // Act
            var success = manager.CheckoutBook("Clean Code");

            // Assert
            Assert.True(success);
            Assert.Equal(0, manager.GetAvailableBookCount());
        }

        [Fact]
        public void CheckoutBook_WhenNotAvailable_ReturnsFalse()
        {
            // Arrange
            var manager = new LibraryManager();
            // Add a book that is already checked out
            manager.AddBook(new Book { Title = "Clean Code", IsAvailable = false });

            // Act
            var success = manager.CheckoutBook("Clean Code");

            // Assert
            Assert.False(success);
        }

        [Fact]
        public void ReturnBook_WhenCheckedOut_ReturnsTrueAndIncreasesCount()
        {
            // Arrange
            var manager = new LibraryManager();
            manager.AddBook(new Book { Title = "The Pragmatic Programmer", IsAvailable = false });

            // Act
            var success = manager.ReturnBook("The Pragmatic Programmer");

            // Assert
            Assert.True(success);
            Assert.Equal(1, manager.GetAvailableBookCount());
        }
    }
}