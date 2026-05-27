using App.Core;
using Data.Models;
using Logic.API;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace App.ViewModels
{
    // Inherits from ObservableObject to automatically update the screen
    public class MainViewModel : ObservableObject
    {
        private readonly ILibraryManager _libraryManager;

        public ObservableCollection<Book> Catalog { get; set; }
        public ObservableCollection<BookCopy> FilteredCopies { get; set; }

        private Book _selectedBook;
        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged(); // Tells the UI the selection changed
                UpdateDetails();     // Refreshes the bottom list
            }
        }

        public ICommand RefreshCommand { get; }

        // Dependency Injection of the Logic Layer
        public MainViewModel(ILibraryManager libraryManager)
        {
            _libraryManager = libraryManager;
            Catalog = new ObservableCollection<Book>();
            FilteredCopies = new ObservableCollection<BookCopy>();

            RefreshCommand = new RelayCommand(o => LoadBooks());

            LoadBooks();
        }

        private void LoadBooks()
        {
            Catalog.Clear();
            var allBooks = _libraryManager.GetAllBooks();
            foreach (var book in allBooks)
            {
                Catalog.Add(book);
            }
        }

        private void UpdateDetails()
        {
            FilteredCopies.Clear();
            if (SelectedBook != null)
            {
                var allCopies = _libraryManager.GetAllBookCopies();
                var bookCopies = allCopies.Where(c => c.BookDetails.Isbn == SelectedBook.Isbn);

                foreach (var copy in bookCopies)
                {
                    FilteredCopies.Add(copy);
                }
            }
        }
    }
}