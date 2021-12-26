using BlazorApp.ApplicationCore.Entities;
using System.Linq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.CustomerTests {

    public class CustomerAddItem {

        [Fact]
        public void AddsBasketItemIfNotPresent() {
            var customer = new Customer();
        }
    }
}