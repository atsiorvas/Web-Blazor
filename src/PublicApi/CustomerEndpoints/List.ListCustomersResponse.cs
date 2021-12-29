using BlazorApp.ApplicationCore.Models;
using System;
using System.Collections.Generic;

namespace BlazorApp.PublicApi.CustomerEndpoints {

    public class ListCustomersResponse : BaseResponse {
        public ListCustomersResponse(Guid correlationId) : base(correlationId) { }

        public ListCustomersResponse() { }

        public IReadOnlyList<CustomerModel> Customers { get; set; } = new List<CustomerModel>();
        public int PageCount { get; set; } = 0;
    }
}