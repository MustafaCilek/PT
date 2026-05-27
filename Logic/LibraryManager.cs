using Data.API;
using Data.Models;
using Logic.API;
using System;
using System.Collections.Generic;

namespace Logic
{
    // Inherit from the new abstract API
    public class LibraryManager : ILibraryManager
    {
        private readonly IDataRepository _repository;

        // Dependency Injection: Inject the abstract Data API
        public LibraryManager(IDataRepository repository)
        {
            _repository = repository;
        }

        public bool CheckoutBook(string readerId, string copyId)
        {
            // The Logic layer uses the abstract API to fetch data
            var copy = _repository.GetBookCopy(copyId);
            var reader = _repository.GetReader(readerId);

            // Business Logic rules
            if (copy != null && copy.IsAvailable && reader != null)
            {
                copy.IsAvailable = false;
                _repository.UpdateBookCopy(copy);

                // Record the State Change Event
                var checkoutEvent = new LibraryEvent
                {
                    EventId = Guid.NewGuid().ToString(),
                    Actor = reader,
                    TargetCopy = copy,
                    Timestamp = DateTime.Now,
                    ActionType = "Checkout"
                };
                _repository.RecordEvent(checkoutEvent);

                return true;
            }
            return false;
        }

        public bool ReturnBook(string copyId)
        {
            var copy = _repository.GetBookCopy(copyId);

            if (copy != null && !copy.IsAvailable)
            {
                copy.IsAvailable = true;
                _repository.UpdateBookCopy(copy);

                var returnEvent = new LibraryEvent
                {
                    EventId = Guid.NewGuid().ToString(),
                    TargetCopy = copy,
                    Timestamp = DateTime.Now,
                    ActionType = "Return"
                };
                _repository.RecordEvent(returnEvent);

                return true;
            }
            return false;
        }

        // --- NEW TASK 2 METHODS FOR THE UI ---

        public IEnumerable<Book> GetAllBooks()
        {
            return _repository.GetAllBooks();
        }

        public IEnumerable<BookCopy> GetAllBookCopies()
        {
            return _repository.GetAllBookCopies();
        }
    }
}