using BlazorApp.ApplicationCore.Interfaces;
using BlazorApp.ApplicationCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
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

        [Inject]
        protected ILogger<Edit> Logger { get; set; }

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

        protected async Task HandleValidSubmitAsync(EditContext editContext) {
            if (editContext == null
                || !editContext.Validate()) {
                Logger.LogInformation("HandleSubmit called: Form is INVALID");
                return;
            }

            if (IsNew) {
                _ = await CustomerService.AddCustomerAsync(Customer);
                UriHelper.NavigateTo("/customer?page=1");
                return;
            }
            await CustomerService.UpdateCustomerAsync(Customer);
            UriHelper.NavigateTo("/customer?page=1");
        }
    }
}