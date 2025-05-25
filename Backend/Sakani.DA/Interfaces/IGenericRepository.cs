using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sakani.DA.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? skip = null,
            int? take = null
        );
        Task<T> GetByIdAsync(object id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(object id);
        Task<bool> ExistsAsync(object id);
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
} 