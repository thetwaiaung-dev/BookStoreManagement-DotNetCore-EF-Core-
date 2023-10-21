using BookManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Dtos
{
    public class PageDto
    {
        public long Page_Id { get; set; }
        public string Content { get; set; }
        public short Page_No { get; set; }

        public long Book_Id { get; set; }
        public Book Book { get; set; } = null!;
    }
}
