using System.Collections.Generic;

namespace BookManagement.Models
{
    public class AuthorResponseModel
    {
        public string? SearchValue {  get; set; }
        public int? AuthorCount { get; set; }
        public int? TotalPages {  get; set; }
        public int? PageSize { get; set; }
        public int? PageNo { get; set; }
        public long? CategoryId { get; set; }
        public List<BookAuthor> Authors { get; set; }
    }
}
