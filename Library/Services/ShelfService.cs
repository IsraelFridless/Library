using Library.Data;
using Library.Models;
using Library.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Library.Services
{
    public class ShelfService : IShelfService
    {
        private readonly ApplicationDBContext _context;

        public ShelfService(ApplicationDBContext dBContext)
        {
            _context = dBContext;
        }

		public async Task<ShelfModel> CreateNewShelf(ShelfVM shelfVM)
		{
            LibraryModel? library = await _context.Libraries.FirstOrDefaultAsync(l => l.Id == shelfVM.LibraryId);
            ShelfModel shelfModel = new() 
            { 
                Height = shelfVM.Height,
                Width = shelfVM.Width,
                LibraryId = shelfVM.LibraryId,
                Library = library
            };

            await _context.Shelves.AddAsync(shelfModel);
            _context.SaveChanges();
            library?.Shelves.Add(shelfModel);
            return shelfModel;
		}

		public List<ShelfModel> GetShelfModelsAsync(LibraryModel libraryModel) => 
            libraryModel.Shelves;

        public async Task<List<ShelfModel>> GetShelvesByLibId(long libId)
        {
            var shelves = await _context.Shelves.Where(s => s.LibraryId == libId).ToListAsync();
            return shelves;
        }
    }
}
