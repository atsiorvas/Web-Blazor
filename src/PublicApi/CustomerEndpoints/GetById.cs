using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using BlazorApp.ApplicationCore.Interfaces;
using BlazorApp.ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace BlazorApp.PublicApi.CustomerEndpoints {

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GetById : BaseAsyncEndpoint
        .WithRequest<GetByIdCustomerRequest>
        .WithResponse<GetByIdCustomerResponse> {

        private readonly ICustomerService _customerService;

        public GetById(ICustomerService customerService) {
            _customerService = customerService;
        }

        [HttpGet("api/customer/get/{CustomerId}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get a Customer by Id",
            Description = "Gets a Customer by Id",
            OperationId = "customer.GetById",
            Tags = new[] { "CustomerEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdCustomerResponse>> HandleAsync(
            [FromRoute] GetByIdCustomerRequest request, CancellationToken cancellationToken) {
            var response = new GetByIdCustomerResponse(request.CorrelationId());

            var customer = await _customerService
                .FindCustomerByIdAsync(request.CustomerId, cancellationToken);

            if (customer is null)
                return NotFound();

            response.Customer = new CustomerModel {
                Id = customer.Id,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                Address = customer.Address,
                City = customer.City,
                Region = customer.Region,
                Country = customer.Country,
                Phone = customer.Phone,
                PostalCode = customer.PostalCode
            };
            return Ok(response);
        }
    }
}