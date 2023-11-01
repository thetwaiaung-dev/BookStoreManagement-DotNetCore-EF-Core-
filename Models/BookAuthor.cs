using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookManagement.Models
{
    [Table("Book_Author")]
    public class BookAuthor
    {
        [Key]
        public  int Author_Id { get; set; }
        public string Author_Name { get; set; }
        public string Author_Photo {  get; set; }

        [JsonIgnore]
        public ICollection<Book> Books{ get; set; }=new List<Book>();
    }
}
