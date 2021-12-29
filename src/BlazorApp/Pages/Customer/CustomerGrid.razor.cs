using BlazorApp.ApplicationCore.Interfaces;
using BlazorApp.ApplicationCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorApp.Pages.Customer {
    public class CustomerGridBase : ComponentBase, IDisposable {

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

        protected override async Task OnInitializedAsync() {
            GetQueryStringValues();
            UriHelper.LocationChanged += OnLocationChanged;
            await LoadCustomers();
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs args) {
            InvokeAsync(async () => {
                await Task.Delay(1);  // wait for blazor to populate route parameters
                GetQueryStringValues();

                await LoadCustomers();
                StateHasChanged();
            });
        }

        protected void GetQueryStringValues() {
            var uri = UriHelper.ToAbsoluteUri(UriHelper.Uri);
            var queryStrings = QueryHelpers.ParseQuery(uri.Query);
            if (queryStrings.TryGetValue("page", out var page)) {
                Page = page;
            }
        }

        protected async Task LoadCustomers() {
            Customers = await CustomerService
                .GetCustomerPaginatedAsync(int.Parse(Page ?? "1"), int.Parse(PageSize ?? "10"));
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
            UriHelper.NavigateTo("/customer?page=" + page);
        }

        protected void AddNew() {
            UriHelper.NavigateTo("/customer/edit/");
        }

        protected async Task SelectedPageSizeAsync(ChangeEventArgs e) {
            PageSize = e.Value.ToString();
            await LoadCustomers();
        }

        public void Dispose() {
            UriHelper.LocationChanged -= OnLocationChanged;
        }
    }
}