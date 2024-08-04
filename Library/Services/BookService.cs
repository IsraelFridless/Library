using Humanizer.Localisation;
using Library.Data;
using Library.Models;
using Library.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using static Library.Utils.NullUtils;

namespace Library.Services
{
	public class BookService : IBookService
    {
        private readonly ApplicationDBContext _context;
        private readonly ISetSevice _setSevice;

        public BookService(ApplicationDBContext context, ISetSevice setSevice)
        {
            _context = context;
            _setSevice = setSevice;
        }

        public async Task<BookModel> AddBook(BookVM bookVM)
        {
            SetModel? set = await _context.Sets
                .Include(s => s.Shelf)
                .Include(s => s.Books)
                .FirstOrDefaultAsync(s => s.Id == bookVM.SetId);
            ShelfModel? shelf = await _context.Shelves.Include(s => s.Sets).ThenInclude(s => s.Books).FirstOrDefaultAsync(s => s.Id == set!.ShelfId);
            BookModel newBook = new()
            {
                Name = bookVM.Name,
                Width = bookVM.Width,
                Height = bookVM.Height,
                SetId = bookVM.SetId,
                Set = set
            };
			if (IsAnyNull(shelf, set))
            {
                throw new Exception("Either the shelf or the set could not be found");
            }

			if (!HasEnoughSpace(shelf!, newBook))
            {
                throw new Exception(
                    "Not enough space in the shelf, maybe try another one..."
                );
            }
            if (HieghtDifferences(shelf!, newBook) < 0.0)
            {
                throw new Exception(
                    "The book is too high for this shelf, maybe try another one..."
                );
            }
            
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            set?.Books.Add(newBook);
            return newBook;
        }

        public double? AllBooksWidth(ShelfModel shelf)
        {
            var totalWidth = shelf?.Sets
                .Select(s => s.Books)
                .ToList()
                .Select(bl => bl
                .Select(b => b.Width)
                .ToList())
                .ToList()
                .Aggregate(0.0, (sum, next) => sum + next
                .Aggregate(0.0, (sum, next) => sum + next));

            ArgumentNullException.ThrowIfNull(nameof(shelf));
            return totalWidth;
        }

        public async Task<List<BookModel>> GetAllBooksBySetId(long setId)
        {
            List<BookModel> books = await _context.Books.Where(b => b.SetId == setId).ToListAsync();
            return books;
        }

        public bool HasEnoughSpace(ShelfModel shelf, BookModel book)
        {
            var totalBooksWidth = AllBooksWidth(shelf);
            var res = shelf?.Width - totalBooksWidth;

            return res > book.Width;
        }

        public double? HieghtDifferences(ShelfModel shelf, BookModel book) => 
            shelf.Height - book.Height;
    }
}
