using WeatherSolution.Models;

namespace ServiceContracts
{
    // Interface for cities service
    public interface IWeatherService
    {
        // Method to get the list of cities weather
        List<CityWeather> GetCities();
    }
}
