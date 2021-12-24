using BlazorApp.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorApp.Pages {
    public class FetchDataBase : ComponentBase {

        protected WeatherForecast[] forecasts;
        [Inject]
        protected WeatherForecastService ForecastService { get; set; }

        protected override async Task OnInitializedAsync() {
            forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
        }
    }
}
