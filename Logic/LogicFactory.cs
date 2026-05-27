using Logic.API;
using Data;

namespace Logic
{
    public static class LogicFactory
    {
        public static ILibraryManager CreateLibraryManager()
        {
            // We get the database interface from the Data Factory
            var repository = DataFactory.CreateDatabase();

            // We inject it into our Logic manager
            return new LibraryManager(repository);
        }
    }
}