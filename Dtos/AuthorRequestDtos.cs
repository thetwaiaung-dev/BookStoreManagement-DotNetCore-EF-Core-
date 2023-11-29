using BookManagement.Models;
using System.Collections;
using System.Collections.Generic;

namespace BookManagement.Dtos
{
    public class AuthorRequestDtos
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorPhoto { get; set; }
        public string PhotoName {  get; set; }
    }
}
