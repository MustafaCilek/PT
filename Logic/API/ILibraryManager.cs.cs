using Data.Models;
using System.Collections.Generic;

namespace Logic.API
{
    public interface ILibraryManager
    {
        bool CheckoutBook(string readerId, string copyId);
        bool ReturnBook(string copyId);

        // These will be used by the UI Master-Detail view
        IEnumerable<Book> GetAllBooks();
        IEnumerable<BookCopy> GetAllBookCopies();
    }
}