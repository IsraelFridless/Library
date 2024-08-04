using Library.Data;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ApplicationDBContext _context;
        public BookController(IBookService bookService, ApplicationDBContext context)
        {
            _bookService = bookService;
            _context = context;
        }
        public async Task<IActionResult> Index(long setId)
        {
            ViewBag.setId = setId;
            return View(await _bookService.GetAllBooksBySetId(setId));
        }

        public IActionResult Create(long setId)
        {
            ViewBag.setId = setId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookVM bookVM)
        {
			try
            {
                await _bookService.AddBook(bookVM);
                return RedirectToAction("Index", new { setId = bookVM.SetId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("createError", ex.Message);
                return View();
            }
        }
    }
}
