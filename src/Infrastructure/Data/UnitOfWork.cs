using BlazorApp.ApplicationCore.Entities;
using BlazorApp.ApplicationCore.Interfaces;
using System;
using System.Threading.Tasks;

namespace BlazorApp.Infrastructure.Data {
    public class UnitOfWork : IUnitOfWork {
        readonly AppDbContext _dbContext;
        public UnitOfWork(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        private IRepository<Customer> customerRepository;

        public IRepository<Customer> CustomerRepository {
            get {
                if (this.customerRepository == null) {
                    this.customerRepository = new EfRepository<Customer>(_dbContext);
                }
                return customerRepository;
            }
        }

        public Task SaveChangesAsync() {
            return _dbContext.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}