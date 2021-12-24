using BlazorApp.ApplicationCore.Interfaces;
using BlazorApp.ApplicationCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorApp.Pages.Customer {
    public class CustomerGridBase : ComponentBase {

        protected PagedResult<CustomerModel> Customers;

        [Inject]
        protected ICustomerService CustomerService { get; set; }

        [Inject]
        protected IJSRuntime Js { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        protected override async Task OnInitializedAsync() {
            Customers = await CustomerService.GetCustomerPaginatedAsync(1, 20);
        }

        protected async Task Delete(Guid customerId) {
            var customer = await CustomerService.FindCustomerByIdAsync(customerId);
            if (customer == null) {
                await Js.InvokeAsync<bool>("prompt", $"Customer with {customerId} wasn't found");
                return;
            }
            if (await Js.InvokeAsync<bool>("confirm", $"Do you want to delete {customerId} Record?")) {
                if (await CustomerService.DeleteCustomerAsync(customer.Id)) {
                    await Js.InvokeAsync<bool>("prompt", $"Customer with {customerId} was deleted");
                } else {
                    await Js.InvokeAsync<bool>("prompt", $"Customer with {customerId} could not be deleted");
                }
            }
            await OnInitializedAsync();
        }

        protected void PagerPageChanged(int page) {
            UriHelper.NavigateTo("/page/" + page);
        }
    }
}