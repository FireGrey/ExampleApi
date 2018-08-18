using System;
using System.Linq;

namespace ExampleApi.Domain.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> Query();
        T Get(long id);
        T Add(T item);
        T Replace(long id, T item);
        bool Delete(long id);
    }
}