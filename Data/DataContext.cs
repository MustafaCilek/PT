// Data/DataContext.cs
using Data.Models;
using System.Collections.Generic;

namespace Data
{
    internal class DataContext
    {
        public List<Reader> Readers { get; set; } = new List<Reader>();

        // "Catalog: a dictionary of the goods descriptions"
        public Dictionary<string, Book> Catalog { get; set; } = new Dictionary<string, Book>();

        public List<BookCopy> CurrentState { get; set; } = new List<BookCopy>();
        public List<LibraryEvent> Events { get; set; } = new List<LibraryEvent>();
    }
}