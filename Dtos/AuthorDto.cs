using BookManagement.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BookManagement.Dtos
{
    public class AuthorDto
    {
        public int Author_Id { get; set; }
        public string Author_Name { get; set; }
        public IFormFile Author_PhotoUrl { get; set; }
        public string Author_Photo {  get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
