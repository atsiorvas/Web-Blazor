using BlazorApp.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlazorApp.Infrastructure.Data {

    public class EfRepository<T> : IRepository<T> where T : BaseEntity {
        readonly AppDbContext _dbContext;
        public EfRepository(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(Guid id) {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<bool> AnyAsync(Guid id) {
            return await _dbContext.Set<T>().Where(s => s.Id == id)
                .AnyAsync();
        }


        public virtual async Task AddAsync(T entity) {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public virtual void Update(T entity) {
            _dbContext.Set<T>().Update(entity);
        }

        public virtual void Delete(T entity) {
            _dbContext.Set<T>().Remove(entity);
        }

        public virtual async Task<IEnumerable<T>> ListAllAsync(Expression<Func<T, bool>> predicate) {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }
    }
}