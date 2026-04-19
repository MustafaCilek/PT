// Data/Models/LibraryEvent.cs
using System;

namespace Data.Models
{
    public class LibraryEvent
    {
        public string EventId { get; set; }
        public Reader Actor { get; set; }
        public BookCopy TargetCopy { get; set; }
        public DateTime Timestamp { get; set; }
        public string ActionType { get; set; } // e.g., "Checkout" or "Return"
    }
}