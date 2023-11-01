using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagement.Models
{
    [Table("Log")]
    public class Log
    {
        [Key]
        public long Log_Id { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseUrl { get; set; }
    }
}
