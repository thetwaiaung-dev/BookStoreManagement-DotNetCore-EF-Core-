using System.Collections.Generic;

namespace BookManagement.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(long id);
        int Create(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
