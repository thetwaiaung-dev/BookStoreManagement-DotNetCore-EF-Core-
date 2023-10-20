using System.Collections.Generic;

namespace BookManagement.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(long id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
