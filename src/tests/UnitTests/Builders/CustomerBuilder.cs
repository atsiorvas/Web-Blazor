using BlazorApp.ApplicationCore.Entities;
using Moq;
using System;

namespace BlazorApp.UnitTests.Builders {

    public class CustomerBuilder {
        private Customer _customer;

        public Guid CustomerId => new Guid("e2efafd2-ed64-ec11-bae7-bc17b8bd0212");

        public CustomerBuilder() {
            _customer = new Customer();
        }

        public Customer Build() => _customer;

        public Customer CreateOneCustomer() {
            var customerMock = new Mock<Customer>();
            _customer = customerMock.Object;
            _customer.Address = "711-2880 Nulla St";
            _customer.City = "Mankato Mississippi";
            _customer.CompanyName = "P.O. Box";
            _customer.ContactName = "P.O. Box Test";
            _customer.Country = "LA";
            _customer.Phone = "2133323232";
            _customer.PostalCode = "71122";
            _customer.Region = "GR";
            return _customer;
        }
    }
}