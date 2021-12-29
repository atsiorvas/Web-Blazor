using System;

namespace BlazorApp.PublicApi.CustomerEndpoints {

    public class GetByIdCustomerRequest : BaseRequest {
        public Guid CustomerId { get; set; }
    }
}