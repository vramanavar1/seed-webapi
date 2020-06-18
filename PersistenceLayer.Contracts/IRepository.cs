using PersistenceLayer.Contracts.Models;
using System;
using System.Collections.Generic;

namespace PersistenceLayer.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(Guid id);
    }
}
