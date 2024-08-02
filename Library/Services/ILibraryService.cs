using Library.Models;
using Library.ViewModels;

namespace Library.Services
{
    public interface ILibraryService
    {
        Task<List<LibraryModel>> GetLibrariesAsync();

        Task<LibraryModel> CreateNewLibrary(string genre);
    }
}
