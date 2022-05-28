using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities;

namespace Data.IRepositories
{
    public interface IGenericRepository
    {
        public interface IGenericRepository<T> where T : BaseEntity
        {
            Task<T> GetByIdAsync(int id);
            Task<IReadOnlyList<T>> ListAllAsync();
            Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate = null);
            public IQueryable<T> Queryable();
            void Add(T entity);
            void AddRange(IEnumerable<T> entities);
            void Update(T entity);
            void Delete(T entity);
        }
    }
}