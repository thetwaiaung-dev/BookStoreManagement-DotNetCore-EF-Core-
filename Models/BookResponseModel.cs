using System.Collections.Generic;

namespace BookManagement.Models
{
    public class BookResponseModel
    {
        public string searchValue {  get; set; }
        public int Book_Count {  get; set; }
        public int TotalPages {  get; set; }
        public int PageSize {  get; set; }
        public int PageNo { get; set; }
        public List<Book> books { get; set; }
        public long CategoryId {  get; set; }
        public int AuthorId {  get; set; }
    }
}
