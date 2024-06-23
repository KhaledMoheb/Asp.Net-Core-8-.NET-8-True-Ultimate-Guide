using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;
using WeatherSolution.Models;

namespace WeatherSolution.Controllers
{
    public class HomeController : Controller
    {
        //private field to hold reference to the service instance
        private readonly IWeatherService _citiesService;

        //Create a constructor and inject IWeatherService
        public HomeController(IWeatherService citiesService)
        {
            _citiesService = citiesService;
        }

        // Route: /
        [Route("/")]
        public IActionResult Index()
        {
            // Get list of city weathers from the service
            List<CityWeather> cityWeatherList = _citiesService.GetCities();
            // Pass the list to the view
            return View(cityWeatherList);
        }

        // Route: /weather/{cityCode}
        [Route("weather/{cityCode}")]
        public IActionResult Weather(string cityCode)
        {
            // Get list of city weathers from the service
            List<CityWeather> cityWeatherList = _citiesService.GetCities();
            // Find the city weather matching the city code
            CityWeather? selectedCityWeather = cityWeatherList.Where(temp => temp.CityUniqueCode.ToLower().Equals(cityCode.ToLower())).FirstOrDefault();
            // Pass the selected city weather to the view
            return View(selectedCityWeather);
        }
    }
}
