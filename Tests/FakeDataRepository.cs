using Data.API;
using Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    // This allows us to test the Logic layer independently of the real Data layer
    public class FakeDataRepository : IDataRepository
    {
        public List<Reader> Readers = new List<Reader>();
        public List<BookCopy> Copies = new List<BookCopy>();
        public List<LibraryEvent> Events = new List<LibraryEvent>();

        public void AddReader(Reader reader) => Readers.Add(reader);
        public Reader GetReader(string id) => Readers.FirstOrDefault(r => r.ReaderId == id);

        public void AddBookToCatalog(Book book) { } // Not needed for current logic tests
        public Book GetBook(string isbn) => null;

        public void AddBookCopy(BookCopy copy) => Copies.Add(copy);
        public BookCopy GetBookCopy(string copyId) => Copies.FirstOrDefault(c => c.CopyId == copyId);

        public void UpdateBookCopy(BookCopy copy)
        {
            var existing = GetBookCopy(copy.CopyId);
            if (existing != null) existing.IsAvailable = copy.IsAvailable;
        }

        public void RecordEvent(LibraryEvent libEvent) => Events.Add(libEvent);
        public IEnumerable<LibraryEvent> GetAllEvents() => Events;
    }
}