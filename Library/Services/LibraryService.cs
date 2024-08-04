using Library.Data;
using Library.Models;
using Library.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using static Library.Utils.NullUtils;


namespace Library.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ApplicationDBContext _context;

        public LibraryService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<LibraryModel> CreateNewLibrary(string genre)
        {
            LibraryModel? res = await _context.Libraries
                .FirstOrDefaultAsync(x => x.Genre == genre);

            if (!IsAnyNull(res))
            {
                throw new Exception(
                    $"Library: {genre} is already exists"
                );
            }
            LibraryModel library = new() { Genre = genre };

            await _context.Libraries.AddAsync(library);
            await _context.SaveChangesAsync();
            return library;
        }

        public Task<List<LibraryModel>> GetLibrariesAsync()
        {
            var libraries = _context.Libraries.ToListAsync();
            if (IsAnyNull(libraries))
            {
                throw new Exception("Libraries not found");
            }
            return libraries;
        }
    }
}
