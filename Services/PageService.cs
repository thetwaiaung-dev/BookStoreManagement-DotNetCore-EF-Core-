using BookManagement.Models;
using BookManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookManagement.Services
{
    public class PageService : IPageRepo
    {
        private readonly BookDbContext _dbContext;

        public PageService(BookDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public int Create(Page entity)
        {
            _dbContext.Pages.Add(entity);
            return _dbContext.SaveChanges();    
        }

        public int Delete(Page entity)
        {
            throw new System.NotImplementedException();
        }

        public List<Page> GetAll()
        {
            var list = _dbContext.Pages.Include(x=>x.Book)
                                        .OrderByDescending(x=>x.Page_Id)
                                        .ToList();
            return list;
        }

        public Page GetById(long id)
        {
            var page = _dbContext.Pages.FirstOrDefault(x => x.Page_Id == id);
            return page;
        }

        public int Update(Page entity)
        {
            return _dbContext.SaveChanges();    
        }
    }
}
