using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library.Data;
using Library.ViewModels;
using Library.Services;


namespace Library.Controllers
{
    public class ShelfController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IShelfService _shelfService;
        public ShelfController(ApplicationDBContext context, IShelfService shelfService)
        {
            _context = context;
            _shelfService = shelfService;
        }
        public async Task<IActionResult> Index(long id)
        {
            ViewBag.libraryId = id;
            return View(await _shelfService.GetShelvesByLibId(id));
        }

        public IActionResult Create(long libraryId) 
        {
            ViewBag.libraryId = libraryId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShelfVM shelfVM)
        {
            try
            {
                await _shelfService.CreateNewShelf(shelfVM);
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("createError", ex.Message);
                return View();
            }
            return RedirectToAction("Index", new {id = shelfVM.LibraryId});
        }
    }
}
