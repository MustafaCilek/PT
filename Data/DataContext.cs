using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    // Inherit from DbContext to make this a real ORM database class
    internal class DataContext : DbContext
    {
        // DbSet represents the actual SQL tables in your database
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Book> Catalog { get; set; }
        public DbSet<BookCopy> CurrentState { get; set; }
        public DbSet<LibraryEvent> Events { get; set; }

        // This method configures the physical SQL database file
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // It will create a local file named "library.db" on your computer
            optionsBuilder.UseSqlite("Data Source=library.db");
        }

        // This method tells the ORM about any special rules for your models
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tell EF Core exactly which properties are the Primary Keys for the SQL tables
            modelBuilder.Entity<Book>().HasKey(b => b.Isbn);
            modelBuilder.Entity<BookCopy>().HasKey(c => c.CopyId);
            modelBuilder.Entity<Reader>().HasKey(r => r.ReaderId);
            modelBuilder.Entity<LibraryEvent>().HasKey(e => e.EventId);
        }
    }
}