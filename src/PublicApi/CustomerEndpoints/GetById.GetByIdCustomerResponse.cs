using BlazorApp.ApplicationCore.Models;
using System;
namespace BlazorApp.PublicApi.CustomerEndpoints {

    public class GetByIdCustomerResponse : BaseResponse {
        public GetByIdCustomerResponse(Guid correlationId) : base(correlationId) {
        }

        public GetByIdCustomerResponse() { }

        public CustomerModel Customer { get; set; }
    }
}
