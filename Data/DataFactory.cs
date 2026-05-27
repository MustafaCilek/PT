using Data.API;
using Data.Models;
using System;
using System.Linq;

namespace Data
{
    public static class DataFactory
    {
        public static IDataRepository CreateDatabase()
        {
            var context = new DataContext();
            var repository = new DataRepository(context);

            // --- DATA SEEDER ---
            // If the database has 0 books, inject some test data!
            if (!repository.GetAllBooks().Any())
            {
                // 1. Create a couple of Master books
                // (Note: If your Book.cs uses different property names like 'Name' instead of 'Title', just update them here)
                var book1 = new Book { Isbn = "111-222-333", Title = "C# in Depth", Author = "Jon Skeet" };
                var book2 = new Book { Isbn = "444-555-666", Title = "Clean Architecture", Author = "Robert C. Martin" };

                // Save books to the database
                repository.AddBookToCatalog(book1);
                repository.AddBookToCatalog(book2);

                // 2. Create some Detail copies for those books
                repository.AddBookCopy(new BookCopy { CopyId = Guid.NewGuid().ToString(), BookDetails = book1, IsAvailable = true });
                repository.AddBookCopy(new BookCopy { CopyId = Guid.NewGuid().ToString(), BookDetails = book1, IsAvailable = false });
                repository.AddBookCopy(new BookCopy { CopyId = Guid.NewGuid().ToString(), BookDetails = book2, IsAvailable = true });
            }

            return repository;
        }
    }
}