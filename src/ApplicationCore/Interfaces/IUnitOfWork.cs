using BlazorApp.ApplicationCore.Entities;
using System;
using System.Threading.Tasks;

namespace BlazorApp.ApplicationCore.Interfaces {
    public interface IUnitOfWork : IDisposable {
        IRepository<Customer> CustomerRepository { get; }

        Task SaveChangesAsync();
    }
}
