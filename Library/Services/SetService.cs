using Library.Data;
using Library.Models;
using Library.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.Services
{
    public class SetService : ISetSevice
    {
        private readonly ApplicationDBContext _context;

        public SetService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<SetModel> CreateNewSet(SetVM setVM)
        {
            ShelfModel? shelf = await _context.Shelves
                .Include(s => s.Sets)
                .FirstOrDefaultAsync(s => s.Id == setVM.ShelfId);

            SetModel? setModel = new () {
                ShelfId = setVM.ShelfId,
                SetName = setVM.SetName,
                Shelf = shelf
            };
            await _context.Sets.AddAsync(setModel);
            await _context.SaveChangesAsync();
            shelf?.Sets.Add(setModel);
            return setModel;
        }

        public async Task<List<SetModel>> GetSetsByShelfId(long shelfId)
        {
            var sets = await _context.Sets.Where(s => s.ShelfId == shelfId).ToListAsync();
            return sets;
        }
    }
}
