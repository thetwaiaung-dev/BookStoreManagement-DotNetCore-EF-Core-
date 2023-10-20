namespace BookManagement.Dtos
{
    public class BookDto
    {
        public long Book_Id { get; set; }
        public string Book_Title { get; set; }
        public string Book_Author { get; set; }
        public int TotalPages { get; set; }
    }
}
