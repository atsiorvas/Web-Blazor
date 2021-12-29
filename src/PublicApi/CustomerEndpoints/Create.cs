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

    public class Create : BaseAsyncEndpoint
        .WithRequest<CreateCustomerRequest>
        .WithResponse<CreateCustomerResponse> {

        private readonly ICustomerService _customerService;

        public Create(ICustomerService customerService) {
            _customerService = customerService;
        }

        [HttpPost("api/customer/create")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Creates a new Customer",
            Description = "Creates a new customer Item",
            OperationId = "customer.create",
            Tags = new[] { "CustomerEndpoints" })
        ]
        public override async Task<ActionResult<CreateCustomerResponse>> HandleAsync(CreateCustomerRequest request, CancellationToken cancellationToken) {
            var response = new CreateCustomerResponse(request.CorrelationId());

            try {
                response.CustomerId = await _customerService
                    .AddCustomerAsync(request.Customer, cancellationToken);
            } catch {
                return response;
            }
            response.Success = true;
            return response;
        }
    }
}