using Library.Models;
using Library.ViewModels;

namespace Library.Services
{
    public interface IShelfService
    {
        List<ShelfModel> GetShelfModels(LibraryModel libraryModel);

        Task<List<ShelfModel>> GetShelvesByLibId(long libId);
        Task<ShelfModel> CreateNewShelf(ShelfVM shelfVM);
    }
}
