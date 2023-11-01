using BookManagement.Models;
using BookManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagement.Services
{
    public class BookService : IBookRepo
    {
        private readonly BookDbContext _dbContext;

        public BookService(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Book entity)
        {
            _dbContext.Book.Add(entity);
            int result = _dbContext.SaveChanges();
            return result;
        }

        public int Delete(Book entity)
        {
            throw new System.NotImplementedException();
        }

        public List<Book> GetAll()
        {
            var books = _dbContext.Book.AsNoTracking()
                .Include(x => x.Author)
                .Include(x => x.Category)
                .Include(x => x.Pages)
                .OrderByDescending(x => x.Book_Id)
                .ToList();
            return books;
        }

        public BookResponseModel GetAllBooks(string searchValue, int pageNo, int pageSize, long categoryId, int authorId)
        {
            var books = from b in _dbContext.Book select b;

            books = books.Include(x => x.Author)
                            .Include(x => x.Category)
                            .Include(x => x.Pages);

            if (!string.IsNullOrEmpty(searchValue))
            {
                books = books.Where(x => x.Book_Title.Contains(searchValue));
            }

            if (categoryId > 0)
            {
                books = books.Where(x => x.Category_Id == categoryId);
            }

            if (authorId > 0)
            {
                books = books.Where(x => x.Author_Id == authorId);
            }

            books = books.OrderByDescending(x => x.Book_Id);
            var items = books.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            int count = books.Count();
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            /* other hand for total pages */
            //int totalPages = (count + pageSize - 1) / pageSize;

            BookResponseModel model = new BookResponseModel()
            {
                Book_Count = count,
                TotalPages = totalPages,
                books = items
            };
            return model;
        }

        public Book GetById(long id)
        {
            Book book = _dbContext.Book.FirstOrDefault(x => x.Book_Id == id);
            return book;
        }

        public int Update(Book entity)
        {
            return _dbContext.SaveChanges();
        }
    }
}
