using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BlazorApp.Pages {
    public class LoginModel : PageModel {
        public async Task OnGetAsync(string redirectUri) {

            if (string.IsNullOrWhiteSpace(redirectUri)) {
                redirectUri = Url.Content("~/");
            }
            // If user is already logged in, we can redirect directly...
            if (HttpContext.User.Identity.IsAuthenticated) {
                Response.Redirect(redirectUri);
            }

            await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme,
                new AuthenticationProperties { RedirectUri = redirectUri });
        }
    }
}
