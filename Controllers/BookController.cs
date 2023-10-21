using BookManagement.Dtos;
using BookManagement.Models;
using BookManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;
        private readonly CategoryService _categoryService;
        private readonly PageService _pageService;

        public BookController(BookService bookService,
                              CategoryService categoryService,
                              PageService pageService)
        {
            _bookService = bookService;
            _categoryService=categoryService;
            _pageService=pageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var books = _bookService.GetAll();
            var categories=_categoryService.GetAll();
            ViewData["books"]=books;
            return View(categories);
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody]BookDto bookDto)
        {
            if (bookDto == null)
            {
                return BadRequest();
            }

            Book model=ChangeModel.Change(bookDto);
            int result=_bookService.Create(model);

            string message = result > 0 ? "Success" : "Failed";
            return Ok(message);
        }

        [HttpPost]
        public IActionResult UpdateBook([FromBody]BookDto bookDto)
        {
            if (bookDto == null)
            {
                return BadRequest();
            }

            Book model = _bookService.GetById(bookDto.Book_Id);
            if(model == null)
            {
                return NotFound();
            }

            model.Book_Title=bookDto.Book_Title;
            model.TotalPages=bookDto.TotalPages;

            int result=_bookService.Update(model);
            string message = result > 0 ? "Success" : "Failed";

            return Ok(message);

        }

        [HttpPost]
        public IActionResult CreatePage([FromBody]PageDto dto)
        {
            if(dto == null)
            {
                return BadRequest();
            }

            Page model=ChangeModel.Change(dto);
            int result=_pageService.Create(model);

            string message = result > 0 ? "Success" : "Failed";
            
            return Ok(message);
        }

        [HttpGet]
        public IActionResult GetAllPage()
        {
            var lst=_pageService.GetAll();

            return Ok(lst);
        }

        [HttpPost]
        public IActionResult UpdatePage([FromBody]PageDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            Page model = _pageService.GetById(dto.Page_Id);
            if (model == null)
            {
                return NotFound();
            }

            model.Content = dto.Content;
            model.Page_No = dto.Page_No;

            int result=_pageService.Update(model);

            string message = result > 0 ? "Success" : "Failed";
            return Ok(message);
        }
    }
}
