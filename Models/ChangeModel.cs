using BookManagement.Dtos;

namespace BookManagement.Models
{
    public static class ChangeModel
    {
        public static Book Change(this BookDto dto)
        {
            if (dto == null)  return null;

            return new Book()
            {
                Book_Id = dto.Book_Id,
                Book_Title = dto.Book_Title,
                TotalPages = dto.TotalPages,
                Pages = dto.Pages,
                Author_Id = dto.Author_Id,
                Author = dto.Author,
                Category_Id = dto.Category_Id,
                Category = dto.Category,
            };
        }

        public static BookDto Change(this Book model)
        {
            if (model == null) return null;

            return new BookDto()
            {
                Book_Id = model.Book_Id,
                Book_Title = model.Book_Title,
                TotalPages = model.TotalPages,
                Pages = model.Pages,
                Author_Id = model.Author_Id,
                Author = model.Author,
                Category_Id = model.Category_Id,
                Category = model.Category,
            };
        }

        public static Page Change(this PageDto dto)
        {
            if (dto == null) return null;
            return new Page()
            {
                Page_Id = dto.Page_Id,
                Content = dto.Content,
                Page_No = dto.Page_No,
                Book_Id = dto.Book_Id,
                Book = dto.Book,
            };
        }

        public static PageDto Change(this Page model)
        {
            if (model == null) return null;
            return new PageDto()
            {
                Page_Id = model.Page_Id,
                Content = model.Content,
                Page_No = model.Page_No,
                Book_Id = model.Book_Id,
                Book = model.Book,
            };
        }

        public static BookAuthor Change(this AuthorDto dto)
        {
            if (dto == null) return null;

            return new BookAuthor()
            {
                Author_Id = dto.Author_Id,
                Author_Name = dto.Author_Name
            };
        }

        public static AuthorDto Change(this BookAuthor model)
        {
            if (model == null) return null;

            return new AuthorDto()
            {
                Author_Id = model.Author_Id,
                Author_Name = model.Author_Name,
                //Author_Photo = model.Author_Photo,
            };
        }
    }
}
