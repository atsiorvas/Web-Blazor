using System;
namespace BlazorApp.PublicApi.CustomerEndpoints {

    public class UpdateCustomerResponse : BaseResponse {
        public UpdateCustomerResponse(Guid correlationId) : base(correlationId) {
        }

        public UpdateCustomerResponse() { }

        public bool Success { get; set; }
    }
}