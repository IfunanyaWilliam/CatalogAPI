using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.Common
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(Guid id);
    }
}
