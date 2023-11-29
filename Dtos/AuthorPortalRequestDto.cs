using BookManagement.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Dtos
{
    public class AuthorPortalRequestDto
    {
        public int Author_Id { get; set; }
        [Required(ErrorMessage ="Author name is required.")]
        public string Author_Name { get; set; }
        public IFormFile AuthorPhoto { get; set; }
        public string Author_Photo {  get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
