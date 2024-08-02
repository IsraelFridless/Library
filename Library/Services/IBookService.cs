using Library.Models;
using Library.ViewModels;

namespace Library.Services
{
    public interface IBookService
    {
        Task<List<BookModel>> GetAllBooksBySetId(long setId);
        Task<BookModel> AddBook(BookVM bookVM);

        double? AllBooksWidth(ShelfModel shelf);
        bool HasEnoughSpace(ShelfModel shelf, BookModel book);
        double? HieghtDifferences(ShelfModel shelf, BookModel book);
	}
}
