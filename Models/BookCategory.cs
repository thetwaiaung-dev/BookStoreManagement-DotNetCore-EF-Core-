using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookManagement.Models
{
    [Table("Book_Category")]
    public class BookCategory
    {
        [Key]
        public long Category_Id { get; set; }
        public string Category_Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]   
        public DateTime ModifiedDate { get; set; }

        [JsonIgnore]
        public ICollection<Book> Books { get; set; }=new List<Book>(); 
    }
}
