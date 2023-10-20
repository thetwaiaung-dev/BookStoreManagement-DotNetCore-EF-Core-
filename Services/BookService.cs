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
        public void Create(Book entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Book entity)
        {
            throw new System.NotImplementedException();
        }

        public List<Book> GetAll()
        {
            var books = _dbContext.Book.Include(x=>x.Author)
                                        .Include(x=>x.Category)
                                        .OrderByDescending(x => x.Book_Id)
                                        .ToList();
            return books;
        }

        public Book GetById(long id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Book entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
