using BookManagement.Dtos;
using BookManagement.Localize;
using BookManagement.Models;
using BookManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BookManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;
        private readonly CategoryService _categoryService;
        private readonly PageService _pageService;
        private readonly AuthorService _authorService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(BookService bookService,
                              CategoryService categoryService,
                              PageService pageService, AuthorService authorService,
                              IWebHostEnvironment webHostEnvironment
                             )
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _pageService = pageService;
            _authorService = authorService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var books = _bookService.GetAll();
            var categories = _categoryService.GetAll();
            ViewData["books"] = books;
            return View(categories);
        }

        [HttpGet]
        public ActionResult AllBooks()
        {
            var books = _bookService.GetAll();
            var categories = _categoryService.GetAll();
            var authors = _authorService.GetAll();

            ViewData["books"] = books;
            ViewData["authors"] = authors;
            return View(categories);
        }

        [HttpGet]
        [Route("/book/category/books/{id}")]
        public ActionResult AllBooksByCategory(long id)
        {
            var books = _bookService.GetAllBooks(null, 1, 9, id, 0);
            var categories = _categoryService.GetAll();
            var authors = _authorService.GetAll();

            var category = _categoryService.GetById(id);

            ViewData["Category"] = category;
            ViewData["CategoryId"] = id;
            ViewData["books"] = books.books;
            ViewData["authors"] = authors;
            return View(categories);
        }

        [HttpGet]
        public ActionResult AllAuthor()
        {
            var categories = _categoryService.GetAll();

            return View(categories);
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] BookDto bookDto)
        {
            if (bookDto == null)
            {
                return BadRequest();
            }

            Book model = ChangeModel.Change(bookDto);
            int result = _bookService.Create(model);

            string message = result > 0 ? "Success" : "Failed";
            return Ok(message);
        }

        [HttpPost]
        public IActionResult UpdateBook([FromBody] BookDto bookDto)
        {
            if (bookDto == null)
            {
                return BadRequest();
            }

            Book model = _bookService.GetById(bookDto.Book_Id);
            if (model == null)
            {
                return NotFound();
            }

            model.Book_Title = bookDto.Book_Title;
            model.TotalPages = bookDto.TotalPages;

            int result = _bookService.Update(model);
            string message = result > 0 ? "Success" : "Failed";

            return Ok(message);

        }

        [HttpPost]
        public IActionResult CreatePage([FromBody] PageDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            Page model = ChangeModel.Change(dto);
            int result = _pageService.Create(model);

            string message = result > 0 ? "Success" : "Failed";

            return Ok(message);
        }

        [HttpGet]
        public IActionResult GetAllPage()
        {
            var lst = _pageService.GetAll();

            return Ok(lst);
        }

        [HttpPost]
        public IActionResult UpdatePage([FromBody] PageDto dto)
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

            int result = _pageService.Update(model);

            string message = result > 0 ? "Success" : "Failed";
            return Ok(message);
        }

        [HttpGet]
        public IActionResult GetAllAuthor()
        {
            List<BookAuthor> authors = _authorService.GetAll();
            return Ok(authors);
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AuthorDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            string folder = "photo/author/";
            folder += Guid.NewGuid().ToString() + "_" + dto.Author_PhotoUrl.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

            BookAuthor model = ChangeModel.Change(dto);
            model.Author_Photo = "/" + folder;
            int result = _authorService.Create(model);

            if (result > 0)
            {
                dto.Author_PhotoUrl.CopyTo(new FileStream(serverFolder, FileMode.Create));
            }

            string message = result > 0 ? "Success" : "Failed";
            return Ok(message);
        }

        [HttpPost]
        public IActionResult UpdateAuthor([FromBody] AuthorDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            BookAuthor model = _authorService.GetById(dto.Author_Id);
            if (model == null)
            {
                return NotFound();
            }

            string serverFolder = null;
            if (model.Author_Photo != dto.Author_PhotoUrl.FileName)
            {
                string folder = "photo/author/";
                folder += Guid.NewGuid().ToString() + "_" + dto.Author_PhotoUrl.FileName;
                serverFolder = Path.Combine(_webHostEnvironment.WebRootPath + folder);

                model.Author_Photo = "/" + folder;
            }

            model.Author_Name = dto.Author_Name;

            int result = _authorService.Update(model);
            if (result > 0 && model.Author_Photo != dto.Author_PhotoUrl.FileName)
            {
                dto.Author_PhotoUrl.CopyTo(new FileStream(serverFolder, FileMode.Create));
            }

            string message = result > 0 ? "Success" : "Failed";
            return Ok(message);
        }
    }
}
