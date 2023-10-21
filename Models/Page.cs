using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookManagement.Models
{
    [Table("Page")]
    public class Page
    {
        [Key]
        public long Page_Id { get; set; }
        [Column("Page_Content")]
        [Required]
        public string Content { get; set; }
        [Required]
        public short Page_No { get; set; }

        [Required]
        public long Book_Id {  get; set; }
        [JsonIgnore]
        public Book Book { get; set; } = null!;

    }
}
