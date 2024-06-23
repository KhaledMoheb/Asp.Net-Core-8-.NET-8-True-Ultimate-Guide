using System.Text.Json;

namespace WeatherSolution.Models
{
    public class CityWeather
    {
        public string CityUniqueCode { get; set; }
        public string CityName { get; set; }
        public DateTime DateAndTime { get; set; }
        public int TemperatureFahrenheit { get; set; }
        public string TemperatureBackgroundClass { get; set; }

        // Convert CityWeather object to JSON string
        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }

        // Set the background class based on temperature
        public void SetTemperatureBackgroundClass()
        {
            if (TemperatureFahrenheit < 44)
                TemperatureBackgroundClass = "blue-back";
            else if (TemperatureFahrenheit <= 74)
                TemperatureBackgroundClass = "green-back";
            else
                TemperatureBackgroundClass = "orange-back";
        }
    }
}
