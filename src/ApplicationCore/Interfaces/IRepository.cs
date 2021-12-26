using System;
using System.Threading.Tasks;

namespace BlazorApp.ApplicationCore.Interfaces {
    public interface IRepository<T> where T : BaseEntity {
        Task AddAsync(T entity);
        Task<bool> AnyAsync(Guid id);
        void Delete(T entity);
        System.Linq.IQueryable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        System.Threading.Tasks.Task<T> GetByIdAsync(Guid id);
        Task<System.Collections.Generic.IEnumerable<T>> ListAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        void Update(T entity);
    }
}