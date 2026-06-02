using Data.API;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    internal class DataRepository : IDataRepository
    {
        private readonly DataContext _context;

        public DataRepository(DataContext context)
        {
            _context = context;
            // This ensures the actual SQL file is created on your hard drive the first time the app runs
            _context.Database.EnsureCreated();
        }

        public void AddReader(Reader reader)
        {
            _context.Readers.Add(reader);
            _context.SaveChanges(); // Commits the INSERT command to SQL
        }

        // LINQ Method Syntax to fetch data
        public Reader GetReader(string id) => _context.Readers.FirstOrDefault(r => r.ReaderId == id);

        public void AddBookToCatalog(Book book)
        {
            _context.Catalog.Add(book);
            _context.SaveChanges();
        }

        public Book GetBook(string isbn) => _context.Catalog.FirstOrDefault(b => b.Isbn == isbn);

        public void AddBookCopy(BookCopy copy)
        {
            _context.CurrentState.Add(copy);
            _context.SaveChanges();
        }

        public BookCopy GetBookCopy(string copyId)
        {
            // .Include() is EF Core's way of doing an SQL JOIN to bring the BookDetails with the Copy
            return _context.CurrentState
                .Include(c => c.BookDetails)
                .FirstOrDefault(c => c.CopyId == copyId);
        }

        public void UpdateBookCopy(BookCopy copy)
        {
            _context.CurrentState.Update(copy);
            _context.SaveChanges(); // Commits the UPDATE command to SQL
        }

        public void RecordEvent(LibraryEvent libEvent)
        {
            _context.Events.Add(libEvent);
            _context.SaveChanges();
        }

        public IEnumerable<Book> GetAllBooksWithQuerySyntax()
        {
            // This is the "Query Syntax" (SQL-like)
            var books = from b in _context.Catalog
                        select b;

            return books.ToList();
        }
        public IEnumerable<LibraryEvent> GetAllEvents() => _context.Events.ToList();

        public IEnumerable<Book> GetAllBooks() => _context.Catalog.ToList();

        public IEnumerable<BookCopy> GetAllBookCopies() =>
            _context.CurrentState.Include(c => c.BookDetails).ToList();
    }
        
}