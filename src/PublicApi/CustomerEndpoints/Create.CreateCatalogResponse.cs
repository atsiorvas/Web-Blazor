using System;

namespace BlazorApp.PublicApi.CustomerEndpoints {

    public class CreateCustomerResponse : BaseResponse {
        public CreateCustomerResponse(Guid correlationId) : base(correlationId) {
        }

        public CreateCustomerResponse() { }

        public bool Success { get; set; }
        public Guid CustomerId { get; set; }
    }
}