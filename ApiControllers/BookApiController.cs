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

        public BookApiController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public IActionResult GetAllBook([FromBody]BookResponseModel responseModel)
        {
            var model = _bookService.GetAllBooks(responseModel.searchValue,responseModel.PageNo,responseModel.PageSize,responseModel.CategoryId,responseModel.AuthorId);
            if(model ==null) return NotFound();

            return Ok(model);
        }

        //[HttpPost]
        //public IActionResult GetAllBookByCategory()
        //{

        //} 
    }
}
