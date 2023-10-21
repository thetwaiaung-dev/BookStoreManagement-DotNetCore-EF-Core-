using BookManagement.Models;
using BookManagement.Repositories;
using Microsoft.EntityFrameworkCore;
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
            int result =_dbContext.SaveChanges();
            return result;
        }

        public int Delete(Book entity)
        {
            throw new System.NotImplementedException();
        }

        public List<Book> GetAll()
        {
            var books = _dbContext.Book.Include(x=>x.Author)
                                        .Include(x=>x.Category)
                                        .Include(x=>x.Pages)
                                        .OrderByDescending(x => x.Book_Id)
                                        .ToList();
            return books;
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
