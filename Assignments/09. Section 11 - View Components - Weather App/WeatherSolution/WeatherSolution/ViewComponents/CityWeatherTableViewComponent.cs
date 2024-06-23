using Microsoft.AspNetCore.Mvc;
using WeatherSolution.Models;

namespace WeatherSolution.ViewComponents
{
    public class CityWeatherTableViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CityWeather cityWeather)
        {
            return View(cityWeather); //invokes a partial view Views/Shared/Components/CityWeatherTable/Default.cshtml
        }
    }
}
