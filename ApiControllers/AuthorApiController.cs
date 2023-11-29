using BookManagement.Dtos;
using BookManagement.Models;
using BookManagement.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using BookManagement.Models.ApiModels;
using System.Text;

namespace BookManagement.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorApiController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AuthorService _authorService;

        public AuthorApiController(IWebHostEnvironment webHostEnvironment, AuthorService authorService)
        {
            _webHostEnvironment = webHostEnvironment;
            _authorService = authorService;
        }

        [HttpPost]
        [Route("CreateAuthor")]
        public IActionResult CreateAuthor([FromBody] AuthorRequestDtos dto)
        {
            ResponseModel model = new ResponseModel();
            if (dto == null)
            {
                model.Data = null;
                model.IsSuccess = false;
                model.Message = "Author is required.";
                return BadRequest(model);
            }

            string folder = _authorService.GetAuthorFolder(dto.PhotoName);

            BookAuthor author = new BookAuthor()
            {
                Author_Name = dto.AuthorName,
                Author_Photo = "/" + folder
            };

            int result = _authorService.Create(author);
            if (result > 0)
            {
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                _authorService.SaveImage(dto.AuthorPhoto, serverFolder);
            }

            model.IsSuccess = result > 0;
            model.Message = result > 0 ? "Success" : "Failed";
            return Ok(model);
        }

        [HttpGet]
        [Route("EditAuthor/{id}")]
        public IActionResult EditAuthor(int id)
        {
            ResponseModel model = new ResponseModel();

            bool isExist = _authorService.IsExist(id);
            if (!isExist)
            {
                model.IsSuccess = false;
                model.Message = "No Data Found.";
                return NotFound(model);
            }

            BookAuthor author = _authorService.GetById(id);
            if (author == null)
            {
                model.IsSuccess = false;
                model.Message = "No Data Found.";
                return NotFound(model);
            }

            string photoName = null;
            if (author.Author_Photo != null)
            {
                photoName = _authorService.GetPhotoName(author.Author_Photo);
            }

            AuthorRequestDtos request = new AuthorRequestDtos()
            {
                Id = author.Author_Id,
                AuthorName = author.Author_Name,
                AuthorPhoto = author.Author_Photo,
                PhotoName = photoName,
            };

            model.Data = request;
            model.IsSuccess = true;
            model.Message = "Success";
            return Ok(model);
        }

        [HttpPut]
        [Route("UpdateAuthor")]
        public IActionResult UpdateAuthor([FromBody] AuthorRequestDtos dto)
        {
            ResponseModel model = new ResponseModel();

            bool isExist = _authorService.IsExist(dto.Id);
            if (!isExist)
            {
                model.IsSuccess = false;
                model.Message = "No Data Found.";
                return NotFound(model);
            }

            BookAuthor author = _authorService.GetById(dto.Id);
            if (author == null)
            {
                model.IsSuccess = false;
                model.Message = "No Data Found.";
                return NotFound(model);
            }

            author.Author_Name = dto.AuthorName;
            string folder = null;
            string authorPhotoPath = author.Author_Photo;
            if (dto.AuthorPhoto != null)
            {
                folder = _authorService.GetAuthorFolder(dto.PhotoName);
                author.Author_Photo = "/" + folder;
            }

            int result = _authorService.Update();
            if (result > 0 && dto.AuthorPhoto != null)
            {
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                _authorService.SaveImage(dto.AuthorPhoto, serverFolder);
                _authorService.RemoveAuthorFolder(_webHostEnvironment.WebRootPath + authorPhotoPath);
            }

            model.IsSuccess = true;
            model.Message = "Success";
            return Ok(model);
        }
    }
}
