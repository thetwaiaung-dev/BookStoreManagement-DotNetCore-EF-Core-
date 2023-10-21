
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookManagement.Models
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public long Book_Id { get; set; }
        [Required]
        public string Book_Title { get; set; }
        [Required]
        public short TotalPages { get; set; }

        public ICollection<Page> Pages { get; set; } =new List<Page>();

        [Required]
        public int Author_Id {  get; set; }
        public BookAuthor Author { get; set; }

        [Required]
        public long Category_Id {  get; set; }
        public BookCategory Category { get; set; } = null!;
    }
}
