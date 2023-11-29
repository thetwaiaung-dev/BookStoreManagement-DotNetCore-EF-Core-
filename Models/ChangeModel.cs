using BookManagement.Dtos;
using Microsoft.JSInterop.Infrastructure;

namespace BookManagement.Models
{
    public static class ChangeModel
    {
        public static Book Change(this BookDto dto)
        {
            if (dto == null) return null;

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

        public static BookAuthor Change(this AuthorPortalRequestDto dto)
        {
            if (dto == null) return null;

            return new BookAuthor()
            {
                Author_Id = dto.Author_Id,
                Author_Name = dto.Author_Name,
                Author_Photo = dto.Author_Photo,
            };
        }

        public static AuthorPortalRequestDto Change(this BookAuthor model)
        {
            if (model == null) return null;

            return new AuthorPortalRequestDto()
            {
                Author_Id = model.Author_Id,
                Author_Name = model.Author_Name,
                Author_Photo = model.Author_Photo,
            };
        }

        public static AuthorPortalRequestDto Change(this AuthorRequestDtos dto)
        {
            if (dto == null) return null;
            return new AuthorPortalRequestDto()
            {
                Author_Id = dto.Id,
                Author_Name = dto.AuthorName,
                Author_Photo = dto.AuthorPhoto,
            };
        }

        public static AuthorRequestDtos ChangeDto(this AuthorPortalRequestDto dto)
        {
            if (dto == null) return null;
            return new AuthorRequestDtos
            {
                Id = dto.Author_Id,
                AuthorName = dto.Author_Name,
                AuthorPhoto = dto.Author_Photo,
            };
        }

        public static BookCategory Change(this BookCategoryDto dto)
        {
            if (dto == null) return null;
            return new BookCategory()
            {
                Category_Id = dto.Category_Id,
                Category_Name = dto.Category_Name,
                CreatedDate = dto.CreatedDate,
                ModifiedDate = dto.ModifiedDate,
            };
        }

        public static BookCategoryDto Change(this BookCategory model)
        {
            if (model == null) return null;
            return new BookCategoryDto()
            {
                Category_Id = model.Category_Id,
                Category_Name = model.Category_Name,
                CreatedDate = model.CreatedDate,
                ModifiedDate = model.ModifiedDate,
            };
        }
    }
}
