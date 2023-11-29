
using BookManagement.Dtos;
using BookManagement.Models;
using BookManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookManagement.AdminController
{
    [Route("admin/[controller]")]
    public class BookController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly AuthorService _authorService;

        public BookController(CategoryService cService, AuthorService aService)
        {
            _categoryService = cService;
            _authorService = aService;
        }

        [HttpGet]
        [Route("create")]
        public IActionResult CreateBook()
        {
            List<BookCategory> categoryList = _categoryService.GetAll();
            List<BookCategoryDto> categoryDtoLst = new List<BookCategoryDto>();
            foreach (var category in categoryList)
            {
                BookCategoryDto dto = category.Change();
                categoryDtoLst.Add(dto);
            }

            List<BookAuthor> authorLst = _authorService.GetAll();
            List<AuthorPortalRequestDto> authorDtoLst = new List<AuthorPortalRequestDto>();
            foreach (var author in authorLst)
            {
                AuthorPortalRequestDto dto = author.Change();
                authorDtoLst.Add(dto);
            }

            ViewData["CategoryLst"] = categoryDtoLst;
            ViewData["AuthorLst"] = authorDtoLst;
            return View();
        }

        [HttpPost]
        [Route("save")]
        public IActionResult SaveBook(BookDto bookDto)
        {
            return RedirectToAction("create");
        }
    }
}
