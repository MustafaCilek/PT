// Data/DataRepository.cs
using Data.API;
using Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class DataRepository : IDataRepository
    {
        private readonly DataContext _context;

        // Dependency Injection: The repository requires a DataContext to be injected
        public DataRepository(DataContext context)
        {
            _context = context;
        }

        public void AddReader(Reader reader) => _context.Readers.Add(reader);
        public Reader GetReader(string id) => _context.Readers.FirstOrDefault(r => r.ReaderId == id);

        public void AddBookToCatalog(Book book) => _context.Catalog.Add(book.Isbn, book);
        public Book GetBook(string isbn) => _context.Catalog.ContainsKey(isbn) ? _context.Catalog[isbn] : null;

        public void AddBookCopy(BookCopy copy) => _context.CurrentState.Add(copy);
        public BookCopy GetBookCopy(string copyId) => _context.CurrentState.FirstOrDefault(c => c.CopyId == copyId);

        public void UpdateBookCopy(BookCopy copy)
        {
            var existing = GetBookCopy(copy.CopyId);
            if (existing != null) existing.IsAvailable = copy.IsAvailable;
        }

        public void RecordEvent(LibraryEvent libEvent) => _context.Events.Add(libEvent);
        public IEnumerable<LibraryEvent> GetAllEvents() => _context.Events;
    }
}