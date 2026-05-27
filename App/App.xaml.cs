using System.Windows;
using App.ViewModels;
using Logic;

namespace App
{
    public partial class LibraryApp : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 1. Get the Logic interface from our clean Factory
            var libraryManager = LogicFactory.CreateLibraryManager();

            // 2. Inject it into the ViewModel
            var mainViewModel = new MainViewModel(libraryManager);

            // 3. Launch the UI Window
            var mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };

            mainWindow.Show();
        }
    }
}