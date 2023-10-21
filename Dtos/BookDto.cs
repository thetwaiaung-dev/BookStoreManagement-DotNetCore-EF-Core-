using BookManagement.Models;
using System.Collections.Generic;

namespace BookManagement.Dtos
{
    public class BookDto
    {
        public long Book_Id { get; set; }
        public string Book_Title { get; set; }
        public short TotalPages { get; set; }

        public ICollection<Page> Pages { get; set; } = new List<Page>();

        public int Author_Id { get; set; }
        public BookAuthor Author { get; set; }

        public long Category_Id { get; set; }
        public BookCategory Category { get; set; } = null!;
    }
}
