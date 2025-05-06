using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DotNetEnv;
using WeatherApi.Models;

namespace WeatherApi.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string ApiKey;
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        // Constructor that loads environment variables from .env file
        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // Load environment variables from the .env file
            Env.Load();

            // Get the API key from the environment variable
            ApiKey = Env.GetString("OPENWEATHER_API_KEY");
        }

        public async Task<WeatherResponse?> GetWeatherAsync(string city)
        {
            try
            {
                var url = $"{BaseUrl}?q={city}&appid={ApiKey}&units=metric";
                return await _httpClient.GetFromJsonAsync<WeatherResponse>(url);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return null;
            }
        }
    }
}
