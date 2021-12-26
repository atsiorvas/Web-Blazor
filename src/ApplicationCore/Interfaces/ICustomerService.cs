using BlazorApp.ApplicationCore.Models;
using System;
using System.Threading.Tasks;

namespace BlazorApp.ApplicationCore.Interfaces {
    public interface ICustomerService {
        Task AddCustomerAsync(CustomerModel customerDto);
        Task<CustomerModel> FindCustomerByIdAsync(Guid customerId);
        Task<bool> DeleteCustomerAsync(Guid customerId);
        Task<PagedResult<CustomerModel>> GetCustomerPaginatedAsync(int page, int pageSize);
        Task<bool> UpdateCustomerAsync(CustomerModel customerModel);
    }
}