using System;

namespace BlazorApp.PublicApi.CustomerEndpoints {

    public class DeleteCustomerRequest : BaseRequest {
        //[FromRoute]
        public Guid CustomerId { get; set; }
    }
}