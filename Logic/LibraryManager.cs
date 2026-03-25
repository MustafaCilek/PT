using Data;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class LibraryManager
    {
        private List<Book> _books = new List<Book>();

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public int GetAvailableBookCount()
        {
            // Using LINQ to quickly count available books
            return _books.Count(b => b.IsAvailable);
        }

        public bool CheckoutBook(string title)
        {
            // Find the first available book with the matching title
            var book = _books.FirstOrDefault(b => b.Title == title && b.IsAvailable);
            if (book != null)
            {
                book.IsAvailable = false;
                return true;
            }
            return false; // Book not found or already checked out
        }

        public bool ReturnBook(string title)
        {
            // Find a checked-out book with the matching title
            var book = _books.FirstOrDefault(b => b.Title == title && !b.IsAvailable);
            if (book != null)
            {
                book.IsAvailable = true;
                return true;
            }
            return false;
        }
    }
}