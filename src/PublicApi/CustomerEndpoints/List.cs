using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using BlazorApp.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlazorApp.PublicApi.CustomerEndpoints {

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class List : BaseAsyncEndpoint
        .WithRequest<ListCustomersRequest>
        .WithResponse<ListCustomersResponse> {
        private readonly ICustomerService _customerService;

        public List(ICustomerService customerService) {
            _customerService = customerService;
        }

        [HttpGet("api/customer/list")]
        [SwaggerOperation(
            Summary = "List Customers",
            Description = "List all customers with pagination",
            OperationId = "Customers.List",
            Tags = new[] { "CustomerEndpoints" })
        ]
        public override async Task<ActionResult<ListCustomersResponse>> HandleAsync([FromQuery] ListCustomersRequest request, CancellationToken cancellationToken) {
            var response = new ListCustomersResponse(request.CorrelationId());

            try {
                var customers = await _customerService
                                .GetCustomerPaginatedAsync(request.PageNumber, request.PageSize);
                response.Customers = customers.Results;
                response.PageCount = customers.PageCount;
            } catch { }
            return response;
        }
    }
}