using BlazorApp.ApplicationCore.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApp.ApplicationCore.Interfaces {
    public interface ICustomerService {
        Task<Guid> AddCustomerAsync(CustomerModel customerDto, CancellationToken token = default);
        Task<CustomerModel> FindCustomerByIdAsync(Guid customerId, CancellationToken token = default);
        Task<bool> DeleteCustomerAsync(Guid customerId, CancellationToken token = default);
        Task<PagedResult<CustomerModel>> GetCustomerPaginatedAsync(int page, int pageSize, CancellationToken token = default);
        Task<bool> UpdateCustomerAsync(CustomerModel customerModel, CancellationToken token = default);
    }
}