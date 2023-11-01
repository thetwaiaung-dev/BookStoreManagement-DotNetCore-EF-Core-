using BookManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Services
{
    public class LogService
    {
        private readonly BookDbContext _dbContext;

        public LogService(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Log log)
        {
            _dbContext.Log.Add(log);
            int result =_dbContext.SaveChanges();

            return result;
        }
    }
}
