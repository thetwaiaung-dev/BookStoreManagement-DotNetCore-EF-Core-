using BookManagement.Models;
using BookManagement.Repositories;
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
           return _dbContent.Author.OrderByDescending(x=>x.Author_Id).ToList();
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
