using BlazorApp.ApplicationCore.Entities;
using Xunit;

namespace BlazorApp.UnitTests.ApplicationCore.Entities.CustomerTests {

    public class CustomerAddItem {

        [Fact]
        public void AddsBasketItemIfNotPresent() {
            var customer = new Customer();
        }
    }
}