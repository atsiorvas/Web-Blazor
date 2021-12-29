using BlazorApp.ApplicationCore.Models;

namespace BlazorApp.PublicApi.CustomerEndpoints {

    public class CreateCustomerRequest : BaseRequest {
        public CustomerModel Customer { get; set; }
    }
}