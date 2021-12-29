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

    public class Delete : BaseAsyncEndpoint
    .WithRequest<DeleteCustomerRequest>
    .WithResponse<DeleteCustomerResponse> {

        private readonly ICustomerService _customerService;

        public Delete(ICustomerService customerService) {
            _customerService = customerService;
        }

        [HttpDelete("api/customer/delete/{customerId}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Deletes a Customer",
            Description = "Deletes a customer Item",
            OperationId = "customer.Delete",
            Tags = new[] { "CustomerEndpoints" })
        ]
        public override async Task<ActionResult<DeleteCustomerResponse>> HandleAsync([FromRoute] DeleteCustomerRequest request, CancellationToken cancellationToken) {
            var response = new DeleteCustomerResponse(request.CorrelationId());
            try {
                response.Success = await _customerService.DeleteCustomerAsync(request.CustomerId, cancellationToken);
            } catch {
                response.Success = false;
            }
            return response;
        }
    }
}