using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Microservices.Base;

namespace User.Microservices.Repositories.Interface
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T> Patch(T entity);
        Task<T> Get(T entity);
        Task<T> Post(T entity);
        Task<T> Put(T entity);
        Task<T> Delete(int id);
    }
}
