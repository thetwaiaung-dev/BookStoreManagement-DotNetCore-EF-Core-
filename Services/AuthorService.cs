using BookManagement.Models;
using BookManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookManagement.Services
{
    public class AuthorService : IAuthorRepo
    {
        private readonly BookDbContext _dbContent;

        public AuthorService(BookDbContext dbContext)
        {
            _dbContent = dbContext;
        }

        public int Create(BookAuthor entity)
        {
            _dbContent.Author.Add(entity);
            return _dbContent.SaveChanges();
        }

        public int Delete(BookAuthor entity)
        {
            throw new System.NotImplementedException();
        }

        public List<BookAuthor> GetAll()
        {
            return _dbContent.Author.OrderByDescending(x => x.Author_Id).ToList();
        }

        public AuthorResponseModel GetAll(string? searchValue, int? pageNo, int? pageSize, long? categoryId)
        {
            var books = from b in _dbContent.Book select b;

            if (categoryId > 0)
            {
                books = books.Where(x => x.Category_Id == categoryId);
            }

            var authors = books.Select(x => x.Author).Distinct();

            if (string.IsNullOrEmpty(searchValue))
            {
                authors = authors.Where(x => x.Author_Name.Contains(searchValue));
            }

            var items = authors.OrderByDescending(x => x.Author_Id)
                                .Skip((int)((pageNo - 1) * pageSize))
                                .Take((int)pageSize).ToList();

            int count = authors.Count();
            int totalPages = (int)((count + pageSize - 1) / pageSize);

            AuthorResponseModel model = new AuthorResponseModel()
            {
                AuthorCount = count,
                TotalPages = totalPages,
                Authors = items
            };
            return model;
        }

        public BookAuthor GetById(long id)
        {
            BookAuthor author = _dbContent.Author.FirstOrDefault(x => x.Author_Id == id);
            return author;
        }

        public int Update(BookAuthor entity)
        {
            return _dbContent.SaveChanges();
        }
    }
}
