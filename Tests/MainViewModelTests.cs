using App.ViewModels;
using Data.Models;
using Logic;
using Xunit;
using System.Linq;

namespace Tests
{
    public class MainViewModelTests
    {
        // Helper method: Builds a perfectly isolated ViewModel using the Fake memory database
        private MainViewModel CreateViewModelWithFakeData()
        {
            var fakeRepo = new FakeDataRepository();

            // 1. Create dummy Master books
            var book1 = new Book { Isbn = "TEST-1", Title = "Test Book 1" };
            var book2 = new Book { Isbn = "TEST-2", Title = "Test Book 2" };
            fakeRepo.AddBookToCatalog(book1);
            fakeRepo.AddBookToCatalog(book2);

            // 2. Create dummy Detail copies
            fakeRepo.AddBookCopy(new BookCopy { CopyId = "C1", BookDetails = book1 });
            fakeRepo.AddBookCopy(new BookCopy { CopyId = "C2", BookDetails = book1 });
            fakeRepo.AddBookCopy(new BookCopy { CopyId = "C3", BookDetails = book2 });

            // 3. Inject Fake Repo -> Logic Manager -> ViewModel
            var libraryManager = new LibraryManager(fakeRepo);
            return new MainViewModel(libraryManager);
        }

        [Fact]
        public void Constructor_ShouldLoadAllBooksIntoCatalog()
        {
            // Arrange & Act
            var viewModel = CreateViewModelWithFakeData();

            // Assert: The ViewModel should have automatically pulled the 2 books
            Assert.Equal(2, viewModel.Catalog.Count);
        }

        [Fact]
        public void SelectedBook_WhenSet_ShouldUpdateFilteredCopies()
        {
            // Arrange
            var viewModel = CreateViewModelWithFakeData();
            var firstBook = viewModel.Catalog.First(b => b.Isbn == "TEST-1");

            // Act: Simulate a user clicking the first book in the UI
            viewModel.SelectedBook = firstBook;

            // Assert: The detail list should update to show exactly 2 copies
            Assert.Equal(2, viewModel.FilteredCopies.Count);
            Assert.All(viewModel.FilteredCopies, c => Assert.Equal("TEST-1", c.BookDetails.Isbn));
        }

        [Fact]
        public void SelectedBook_WhenChanged_ShouldRefreshFilteredCopies()
        {
            // Arrange
            var viewModel = CreateViewModelWithFakeData();
            var book1 = viewModel.Catalog.First(b => b.Isbn == "TEST-1");
            var book2 = viewModel.Catalog.First(b => b.Isbn == "TEST-2");

            // Act: Simulate user clicking book 1, then changing their mind and clicking book 2
            viewModel.SelectedBook = book1;
            viewModel.SelectedBook = book2;

            // Assert: The detail list should clear the old copies and only show the 1 copy for Book 2
            Assert.Single(viewModel.FilteredCopies);
            Assert.Equal("C3", viewModel.FilteredCopies.First().CopyId);
        }
    }
}