using BlazorApp.ApplicationCore.Models;

namespace BlazorApp.PublicApi.CustomerEndpoints {

    public class UpdateCustomerRequest : BaseRequest {
        public CustomerModel Customer { get; set; }
    }
}