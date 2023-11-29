using BookManagement.Dtos;
using BookManagement.Models.ApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Controllers
{
    public class BaseController : Controller
    {
        private const string baseUrl = "https://localhost:5001/api/";

        #region author url
        public const string authorUpdateUrl = "AuthorApi/UpdateAuthor";
        public const string authorCreateUrl = "AuthorApi/CreateAuthor";
        public const string authorEditUrl = "AuthorApi/EditAuthor";
        #endregion

        public async Task<ResponseModel> GetApi(string path)
        {
            ResponseModel model = new ResponseModel();  

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(baseUrl + path);
                string jsonString = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<ResponseModel>(jsonString);
            }
            return model;
        }

        public async Task<ResponseModel> PostApi(string content, string path)
        {
            ResponseModel model = new ResponseModel();

            using (HttpClient client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(baseUrl + path, httpContent);
                string jsonString = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<ResponseModel>(jsonString);
            }
            return model;
        }

        public async Task<ResponseModel> PutApi(string content, string path)
        {
            ResponseModel model = new ResponseModel();

            using (HttpClient client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(baseUrl + path, httpContent);
                string jsonString = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<ResponseModel>(jsonString);
            }
            return model;
        }

        public string ConvertToBase64(IFormFile file)
        {
            string base64String = null;
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);

                    byte[] fileBytes = memoryStream.ToArray();
                    base64String = Convert.ToBase64String(fileBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting IFormFile to Base64: {ex.Message}");
                return null;
            }
            return base64String;
        }

        public object DecodeBase64(string base64String)
        {
            var base64EncodedByte = Convert.FromBase64String(base64String);

            object decodeObject = Encoding.UTF8.GetString(base64EncodedByte);
            return decodeObject;
        }

    }
}
