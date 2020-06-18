using PersistenceLayer.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersistenceLayer.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
    }
}
