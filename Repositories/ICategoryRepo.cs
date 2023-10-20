using BookManagement.Models;
using System.Collections.Generic;

namespace BookManagement.Repositories
{
    public interface ICategoryRepo
    {
        List<BookCategory> GetAll();
        BookCategory GetById(long id);
        void Create(BookCategory category);
        void Update(BookCategory category);
    }
}
