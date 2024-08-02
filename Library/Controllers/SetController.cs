using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library.Data;
using Library.ViewModels;
using Library.Services;

namespace Library.Controllers
{
    public class SetController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly ISetSevice _setService;

        public SetController(ApplicationDBContext context, ISetSevice setService)
        {
            _setService = setService;
            _context = context;
        }
        public async Task<IActionResult> Index(long shelfId)
        {
            ViewBag.ShelfId = shelfId;
            return View(await _setService.GetSetsByShelfId(shelfId));
        }

        public IActionResult Create(long shelfId)
        {
            
            ViewBag.ShelfId = shelfId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SetVM setVM)
        {
            try
            {
                await _setService.CreateNewSet(setVM);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("createError", ex.Message);
                return View();
            }
            return RedirectToAction("Index", new { shelfId = setVM.ShelfId });
        }
    }
}
