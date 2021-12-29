using System;
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
    public class Update : BaseAsyncEndpoint
    .WithRequest<UpdateCustomerRequest>
    .WithResponse<UpdateCustomerResponse> {
        private readonly ICustomerService _customerService;

        public Update(ICustomerService customerService) {
            _customerService = customerService;
        }

        [HttpPut("api/customer/update/")]
        [SwaggerOperation(
            Summary = "Updates a Customer",
            Description = "Updates a Customer Item",
            OperationId = "customer.update",
            Tags = new[] { "CustomerEndpoints" })
        ]
        public override async Task<ActionResult<UpdateCustomerResponse>> HandleAsync(
            UpdateCustomerRequest request, CancellationToken cancellationToken) {
            var response = new UpdateCustomerResponse(request.CorrelationId());

            try {
                response.Success = await _customerService
                    .UpdateCustomerAsync(request.Customer);
            } catch {
                response.Success = false;
            }
            return response;
        }
    }
}