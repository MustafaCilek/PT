// Data/API/IDataRepository.cs
using Data.Models;
using System.Collections.Generic;

namespace Data.API
{
    public interface IDataRepository
    {
        // Users
        void AddReader(Reader reader);
        Reader GetReader(string id);

        // Catalog
        void AddBookToCatalog(Book book);
        Book GetBook(string isbn);

        // Process State
        void AddBookCopy(BookCopy copy);
        BookCopy GetBookCopy(string copyId);
        void UpdateBookCopy(BookCopy copy);

        // Events
        void RecordEvent(LibraryEvent libEvent);
        IEnumerable<LibraryEvent> GetAllEvents();
    }
}