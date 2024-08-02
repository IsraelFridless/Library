using Library.Data;
using Library.Services;
using Library.ViewModels;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Library.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;
        private readonly ApplicationDBContext _context;

        public LibraryController(ILibraryService libraryService, ApplicationDBContext context)
        {
            _libraryService = libraryService;
            _context = context;
        }

        public async Task<IActionResult> Index() =>
            View(await _libraryService.GetLibrariesAsync());


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string genre)
        {
            try
            {
                await _libraryService.CreateNewLibrary(genre);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("createError", ex.Message);
                return View();
            }
        }
    }
}
