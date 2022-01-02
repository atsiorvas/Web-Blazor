using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazorApp.Data;
using BlazorApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BlazorApp.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using BlazorApp.ApplicationCore.Services;

namespace BlazorApp {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<AppDbContext>(c =>
                    c.UseSqlServer(Configuration.GetConnectionString("AppDbConnection")));

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddAuthenticationCookie(Configuration);
            services.AddSingleton<WeatherForecastService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }

    public static class ModuleExtensions {
        public static void AddAuthenticationCookie(this IServiceCollection services, IConfiguration configuration) {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            services.AddAuthentication(options => {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options => {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.SignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.Authority = configuration["IdentityServiceSettings:AuthorityUrl"];
                options.ClientId = configuration["IdentityServiceSettings:ClientId"];
                options.ClientSecret = configuration["IdentityServiceSettings:ClientSecret"];

                // When set to code, the middleware will use PKCE protection
                options.ResponseType = "code";
                options.UsePkce = true;

                foreach (var scope in configuration.GetSection("IdentityServiceSettings:Scopes")
                                                        .Get<string[]>())
                    options.Scope.Add(scope);

                // Save the tokens we receive from the IDP
                options.SaveTokens = true;

                // It's recommended to always get claims from the 
                // UserInfoEndpoint during the flow. 
                options.GetClaimsFromUserInfoEndpoint = true;
            });
        }
    }
}