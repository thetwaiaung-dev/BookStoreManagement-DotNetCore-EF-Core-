using BookManagement.Dtos;
using BookManagement.Models;
using BookManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace BookManagement.Controllers
{
    public class AuthorController : BaseController
    {
        private readonly CategoryService _categoryService;

        public AuthorController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();

            return View(categories);
        }

        public IActionResult CreateAuthor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveAuthor(AuthorPortalRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateAuthor", dto);
            }

            dto.Author_Photo = ConvertToBase64(dto.AuthorPhoto);
            AuthorRequestDtos requestModel = dto.ChangeDto();
            requestModel.PhotoName = dto.AuthorPhoto.FileName;

            string content = JsonConvert.SerializeObject(requestModel);
            var responseModel = PostApi(content, authorCreateUrl).Result;

            TempData["Message"] = responseModel.Message;
            if (responseModel.IsSuccess == false)
            {
                return View("CreateAuthor", dto);
            }

            return Redirect("/Book");
        }

        public IActionResult EditAuthor(int id)
        {
            var response = GetApi(authorEditUrl + "/" + id).Result;
            TempData["Message"] = response.Message;
            if (response.IsSuccess == false)
            {
                return Redirect("/Book");
            }

            AuthorRequestDtos dto = JsonConvert.DeserializeObject<AuthorRequestDtos>(response.Data.ToString());
            AuthorPortalRequestDto model = ChangeModel.Change(dto);
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateAuthor(AuthorPortalRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View("EditAuthor", dto);
            }

            string photoName = null;
            if (dto.AuthorPhoto != null)
            {
                dto.Author_Photo = ConvertToBase64(dto.AuthorPhoto);
                photoName = dto.AuthorPhoto.FileName;
            }
            AuthorRequestDtos requestModel = dto.ChangeDto();
            requestModel.PhotoName = photoName;

            string content = JsonConvert.SerializeObject(requestModel);
            var responseModel = PutApi(content, authorUpdateUrl).Result;

            TempData["Message"] = responseModel.Message;
            if (responseModel.IsSuccess == false)
            {
                return View("CreateAuthor", dto);
            }

            return Redirect("/Book");
        }
    }
}
