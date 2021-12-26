using System.Threading.Tasks;
using BlazorApp.ApplicationCore.Entities;
using BlazorApp.ApplicationCore.Interfaces;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.CustomerServiceTests {

    public class AddItemToBasket {
        private readonly Mock<IRepository<Customer>> _mockCustomerRepo = new Mock<IRepository<Customer>>();

        [Fact]
        public async Task InvokesBasketRepositoryGetBySpecAsyncOnce() {
            var customer = new Customer();
        }
    }
}