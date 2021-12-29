using System;

namespace BlazorApp.PublicApi.CustomerEndpoints {

    public class DeleteCustomerResponse : BaseResponse {
        public DeleteCustomerResponse(Guid correlationId) : base(correlationId) { }

        public DeleteCustomerResponse() { }

        public bool Success { get; set; }
    }
}