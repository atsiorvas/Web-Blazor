using System;
using System.Threading.Tasks;
using BlazorApp.ApplicationCore.Entities;
using BlazorApp.ApplicationCore.Interfaces;
using BlazorApp.ApplicationCore.Models;
using BlazorApp.ApplicationCore.Services;
using Moq;
using Xunit;

namespace BlazorApp.UnitTests.ApplicationCore.Services {

    public class CustomerServiceTest {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();

        public CustomerServiceTest() { }

        [Fact]
        public async Task InvokesCustomerRepositoryAdd() {
            bool wasCalled = false;
            Mock<IRepository<Customer>> repositoryMock = new Mock<IRepository<Customer>>();
            repositoryMock.Setup(r => r.AddAsync(
                It.IsAny<Customer>()))
                .Callback(() => wasCalled = true);

            _mockUnitOfWork.Setup(uow => uow.CustomerRepository).Returns(repositoryMock.Object);

            var customerService = new CustomerService(_mockUnitOfWork.Object);

            var customer = new CustomerModel {
                CompanyName = "Test LTD"
            };

            await customerService.AddCustomerAsync(customer);

            Assert.True(wasCalled, "Add customer was not called.");
            //repositoryMock.Verify(x => x.AddAsync(customer), Times.Once);
        }

        [Fact]
        public async Task InvokesCustomerRepositoryDelete() {
            bool wasCalled = false;
            Mock<IRepository<Customer>> repositoryMock = new Mock<IRepository<Customer>>();
            repositoryMock.Setup(r => r.Delete(
                It.IsAny<Customer>()))
                .Callback(() => wasCalled = true);

            _mockUnitOfWork.Setup(uow => uow.CustomerRepository).Returns(repositoryMock.Object);

            var customerService = new CustomerService(_mockUnitOfWork.Object);
            var id = new Guid("e2efafd2-ed64-ec11-bae7-bc17b8bd0212");

            await customerService.DeleteCustomerAsync(id);

            Assert.True(wasCalled, "Delete customer was not called.");
        }
    }
}