// Data/Models/BookCopy.cs
namespace Data.Models
{
    public class BookCopy
    {
        public string CopyId { get; set; }
        public Book BookDetails { get; set; } // Reference to the Catalog
        public bool IsAvailable { get; set; } = true;
    }
}