using BlazorApp.ApplicationCore.Interfaces;
using BlazorApp.ApplicationCore.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazorApp.ApplicationCore.Entities;
using System.Threading;

namespace BlazorApp.ApplicationCore.Services {

    public class CustomerService : ICustomerService {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> UpdateCustomerAsync(CustomerModel customerModel, CancellationToken token = default) {
            if (customerModel == null) {
                throw new ArgumentNullException(nameof(customerModel));
            }
            var customerEntity = await _unitOfWork.CustomerRepository.GetByIdAsync(customerModel.Id);
            if (customerEntity == null) {
                return false;
            }
            customerEntity.CompanyName = customerModel.CompanyName;
            customerEntity.City = customerModel.City;
            customerEntity.Address = customerModel.Address;
            customerEntity.Phone = customerModel.Phone;
            customerEntity.ContactName = customerModel.ContactName;
            customerEntity.Country = customerModel.Country;
            customerEntity.PostalCode = customerModel.PostalCode;
            customerEntity.Region = customerModel.Region;

            await _unitOfWork.SaveChangesAsync(token);
            return true;
        }

        public async Task<Guid> AddCustomerAsync(CustomerModel customerDto, CancellationToken token = default) {
            var customer = new Customer {
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
            await _unitOfWork.SaveChangesAsync(token);
            return customer.Id;
        }

        public async Task<PagedResult<CustomerModel>> GetCustomerPaginatedAsync(int page, int pageSize, CancellationToken token = default) {
            var customerQuery = _unitOfWork.CustomerRepository.Find(_ => true);

            var result = new PagedResult<CustomerModel> {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = await customerQuery.CountAsync()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;

            var customers = await customerQuery
                                    .Skip(skip)
                                    .Take(pageSize)
                                    .ToListAsync();

            result.Results = customers
                .Select(customer => new CustomerModel {
                    Id = customer.Id,
                    CompanyName = customer.CompanyName,
                    ContactName = customer.ContactName,
                    Address = customer.Address,
                    City = customer.City,
                    Region = customer.Region,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country,
                    Phone = customer.Phone
                }).ToList()
                .AsReadOnly();

            return result;
        }

        public async Task<CustomerModel> FindCustomerByIdAsync(Guid customerId, CancellationToken token = default) {
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

        public async Task<bool> DeleteCustomerAsync(Guid customerId, CancellationToken token = default) {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            if (customer is null)
                return false;
            _unitOfWork.CustomerRepository.Delete(customer);
            await _unitOfWork.SaveChangesAsync(token);
            return true;
        }
    }
}