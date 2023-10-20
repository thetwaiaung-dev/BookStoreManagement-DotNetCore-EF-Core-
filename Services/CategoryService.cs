using BookManagement.Models;
using BookManagement.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BookManagement.Services
{
    public class CategoryService : ICategoryRepo
    {
        private readonly BookDbContext _dbContext;

        public CategoryService(BookDbContext bookDbContext)
        {
            _dbContext= bookDbContext;
        }
        public void Create(BookCategory category)
        {
            throw new System.NotImplementedException();
        }

        public List<BookCategory> GetAll()
        {
            var categories = _dbContext.Category.ToList();
            return categories;
        }

        public BookCategory GetById(long id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(BookCategory category)
        {
            throw new System.NotImplementedException();
        }
    }
}
