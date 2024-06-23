using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using WeatherSolution.Models;

namespace WeatherSolution.Controllers
{
    public class HomeController : Controller
    {
        // Route: /
        [Route("/")]
        public IActionResult Index([FromServices] ICitiesService citiesService)
        {
            // Get list of city weathers from the service
            List<CityWeather> cityWeatherList = citiesService.GetCities();
            // Pass the list to the view
            return View(cityWeatherList);
        }

        // Route: /weather/{cityCode}
        [Route("weather/{cityCode}")]
        public IActionResult Weather(string cityCode, [FromServices] ICitiesService citiesService)
        {
            // Get list of city weathers from the service
            List<CityWeather> cityWeatherList = citiesService.GetCities();
            // Find the city weather matching the city code
            CityWeather? selectedCityWeather = cityWeatherList.Where(temp => temp.CityUniqueCode.ToLower().Equals(cityCode.ToLower())).FirstOrDefault();
            // Pass the selected city weather to the view
            return View(selectedCityWeather);
        }
    }
}
