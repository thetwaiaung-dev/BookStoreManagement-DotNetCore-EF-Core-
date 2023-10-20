using BookManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;
        private readonly CategoryService _categoryService;

        public BookController(BookService bookService,
                              CategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService=categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var books = _bookService.GetAll();
            var categories=_categoryService.GetAll();
            ViewData["books"]=books;
            return View(categories);
        }
    }
}
