using BlazorApp.ApplicationCore.Interfaces;
using BlazorApp.ApplicationCore.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Services {

    public class CustomerService : ICustomerService {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task AddCustomerAsync(CustomerModel customerDto) {
            var customer = new BlazorApp.ApplicationCore.Entities.Customer {
                CompanyName = customerDto.CompanyName,
                ContactName = customerDto.ContactName,
                Address = customerDto.Address,
                City = customerDto.City,
                Region = customerDto.Region,
                PostalCode = customerDto.PostalCode,
                Country = customerDto.Country,
                Phone = customerDto.Phone
            };

            await _unitOfWork.CustomerRepository.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResult<CustomerModel>> GetCustomerPaginatedAsync(int page, int pageSize) {
            var customers = await _unitOfWork.CustomerRepository.ListAllAsync(_ => true);
            if (customers == null) {
                throw new ArgumentException(nameof(customers));
            }

            var result = new PagedResult<CustomerModel> {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = customers.Count()
            };
            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Results = customers.Select(customer => new CustomerModel {
                Id = customer.Id,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                Address = customer.Address,
                City = customer.City,
                Region = customer.Region,
                PostalCode = customer.PostalCode,
                Country = customer.Country,
                Phone = customer.Phone
            }).ToList();

            return result;
        }

        public async Task<CustomerModel> FindCustomerByIdAsync(Guid customerId) {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            if (customer != null) {
                return new CustomerModel {
                    Id = customer.Id,
                    CompanyName = customer.CompanyName,
                    ContactName = customer.ContactName,
                    Address = customer.Address,
                    City = customer.City,
                    Region = customer.Region,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country,
                    Phone = customer.Phone
                };
            }
            return null;
        }

        public async Task<bool> DeleteCustomerAsync(Guid customerId) {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            if (customer is null)
                return false;
            _unitOfWork.CustomerRepository.Delete(customer);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}