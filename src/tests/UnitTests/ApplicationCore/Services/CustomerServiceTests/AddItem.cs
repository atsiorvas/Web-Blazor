using System.Threading.Tasks;
using BlazorApp.ApplicationCore.Entities;
using BlazorApp.ApplicationCore.Interfaces;
using Moq;
using Xunit;

namespace BlazorApp.UnitTests.ApplicationCore.Services.CustomerServiceTests {

    public class AddItem {
        private readonly Mock<IRepository<Customer>> _mockCustomerRepo = new Mock<IRepository<Customer>>();

        [Fact]
        public async Task InvokesBasketRepositoryGetBySpecAsyncOnce() {
            var customer = new Customer();
        }
    }
}