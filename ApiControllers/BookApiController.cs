using BookManagement.Models;
using BookManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookManagement.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookApiController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly AuthorService _authorService;

        public BookApiController(BookService bookService, AuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }

        [HttpPost]
        [Route("/api/bookApi/get-all-books")]
        public IActionResult GetAllBook([FromBody] BookResponseModel responseModel)
        {
            var model = _bookService.GetAllBooks(responseModel.searchValue, responseModel.PageNo, responseModel.PageSize, responseModel.CategoryId, responseModel.AuthorId);
            if (model == null) return NotFound();

            return Ok(model);
        }

        [HttpPost]
        [Route("/api/bookApi/get-all-authors")]
        public IActionResult GetAllAuthor([FromBody] AuthorResponseModel responseModel)
        {
            var model = _authorService.GetAll(responseModel.SearchValue, responseModel.PageNo, responseModel.PageSize, responseModel.CategoryId);
            if (model == null) return NotFound();

            return Ok(model);
        }

        //[HttpPost]
        //public IActionResult GetAllBookByCategory()
        //{

        //} 
    }
}
