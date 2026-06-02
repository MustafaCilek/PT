using Data.API;
using Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class FakeDataRepository : IDataRepository
    {
        // 1. The Fake "Database" Memory
        public List<Reader> Readers { get; set; } = new List<Reader>();
        public Dictionary<string, Book> Catalog { get; set; } = new Dictionary<string, Book>();
        public List<BookCopy> CurrentState { get; set; } = new List<BookCopy>();
        public List<LibraryEvent> Events { get; set; } = new List<LibraryEvent>();

        // 2. The Original Contract Methods (From Task 1)
        public void AddReader(Reader reader) => Readers.Add(reader);
        public Reader GetReader(string id) => Readers.FirstOrDefault(r => r.ReaderId == id);

        public void AddBookToCatalog(Book book) => Catalog.Add(book.Isbn, book);
        public Book GetBook(string isbn) => Catalog.ContainsKey(isbn) ? Catalog[isbn] : null;

        public void AddBookCopy(BookCopy copy) => CurrentState.Add(copy);
        public BookCopy GetBookCopy(string copyId) => CurrentState.FirstOrDefault(c => c.CopyId == copyId);

        public void UpdateBookCopy(BookCopy copy)
        {
            var existing = GetBookCopy(copy.CopyId);
            if (existing != null) existing.IsAvailable = copy.IsAvailable;
        }

        public void RecordEvent(LibraryEvent libEvent) => Events.Add(libEvent);
        public IEnumerable<LibraryEvent> GetAllEvents() => Events;

        // 3. The New UI Methods (From Task 2)
        public IEnumerable<Book> GetAllBooks() => Catalog.Values.ToList();
        public IEnumerable<BookCopy> GetAllBookCopies() => CurrentState;

        public IEnumerable<Book> GetAllBooksWithQuerySyntax()
        {
            // Since this is a simple List/Dictionary, we can just return the values 
            // converted to a list. This satisfies the interface requirement.
            return Catalog.Values.ToList();
        }
    }
}