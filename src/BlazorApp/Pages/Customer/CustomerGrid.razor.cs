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

        [Parameter]
        public string Page { get; set; } = "1";

        protected string PageSize { get; set; } = "10";

        //protected override async Task OnInitializedAsync() {
        //    await LoadCustomers();
        //}

        protected override async Task OnParametersSetAsync() {
            await LoadCustomers();
        }

        protected async Task LoadCustomers() {
            Customers = await CustomerService.GetCustomerPaginatedAsync(int.Parse(Page ?? "1"), int.Parse(PageSize ?? "10"));
        }

        protected async Task Delete(Guid customerId) {
            var customer = await CustomerService.FindCustomerByIdAsync(customerId);
            if (customer == null) {
                await Js.InvokeVoidAsync("alert", $"Customer with {customerId} wasn't found");
                return;
            }
            if (await Js.InvokeAsync<bool>("confirm", $"Do you want to delete {customerId} Record?")) {
                if (await CustomerService.DeleteCustomerAsync(customer.Id)) {
                    await Js.InvokeVoidAsync("alert", $"Customer with {customerId} was deleted");
                } else {
                    await Js.InvokeVoidAsync("alert", $"Customer with {customerId} could not be deleted");
                }
            }
            await LoadCustomers();
        }

        protected void PagerPageChanged(int page) {
            UriHelper.NavigateTo("/customer/" + page);
        }

        protected void AddNew() {
            UriHelper.NavigateTo("/customer/edit/");
        }

        protected async Task SelectedPageSizeAsync(ChangeEventArgs e) {
            PageSize = e.Value.ToString();
            await LoadCustomers();
        }
    }
}