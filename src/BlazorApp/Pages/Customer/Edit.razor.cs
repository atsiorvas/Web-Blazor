using BlazorApp.ApplicationCore.Interfaces;
using BlazorApp.ApplicationCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorApp.Pages.Customer {
    public class EditBase : ComponentBase {
        [Inject]
        protected ICustomerService CustomerService { get; set; }

        [Inject]
        protected IJSRuntime Js { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Parameter]
        public Guid Id { get; set; } = Guid.Empty;
        protected string PageTitle { get; private set; }
        protected bool IsNew => Id == Guid.Empty;
        protected CustomerModel Customer { get; set; }

        protected override async Task OnInitializedAsync() {
            if (IsNew) {
                PageTitle = "Add customer";
                Customer = new CustomerModel();
            } else {
                PageTitle = "Edit customer";
                Customer = await CustomerService.FindCustomerByIdAsync(Id);
            }
        }

        protected async Task SaveAsync() {
            if (IsNew) {
                await CustomerService.AddCustomerAsync(Customer);
                UriHelper.NavigateTo("/customer/");
                return;
            }
            await CustomerService.UpdateCustomerAsync(Customer);
            UriHelper.NavigateTo("/customer/");
        }
    }
}