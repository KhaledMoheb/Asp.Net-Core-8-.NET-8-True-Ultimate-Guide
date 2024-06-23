using ServiceContracts;
using WeatherSolution.Models;

namespace Services
{
    public class CitiesService : ICitiesService
    {
        private List<CityWeather> _citiesWeather;

        // Initializing the list of city weathers
        public CitiesService()
        {
            _citiesWeather = new List<CityWeather>
            {
                new CityWeather{
                    CityUniqueCode = "LDN",
                    CityName = "London",
                    DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),
                    TemperatureFahrenheit = 33
                },
                new CityWeather{
                    CityUniqueCode = "NYC",
                    CityName = "New York",
                    DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),
                    TemperatureFahrenheit = 60
                },
                new CityWeather{
                    CityUniqueCode = "PAR",
                    CityName = "Paris",
                    DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),
                    TemperatureFahrenheit = 82
                }
            };

            // Set temperature background class for each city weather
            _citiesWeather.ForEach(cityWeather =>
            {
                cityWeather.SetTemperatureBackgroundClass();
            });
        }

        // Method to get the list of city weathers
        public List<CityWeather> GetCities()
        {
            return _citiesWeather;
        }
    }
}
