using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DotNetEnv;
using WeatherApi.Models;
using Microsoft.Extensions.Logging; // ✅ Import ILogger

namespace WeatherApi.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherService> _logger; // ✅ Add logger field
        private readonly string ApiKey;
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        // ✅ Inject ILogger via constructor
        public WeatherService(HttpClient httpClient, ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            Env.Load(); // ✅ Load environment variables from .env
            ApiKey = Env.GetString("OPENWEATHER_API_KEY");
        }

        // ✅ Method to fetch weather data
        public async Task<WeatherResponse?> GetWeatherAsync(string city)
        {
            try
            {
                // ✅ Log the API call
                _logger.LogInformation($"Fetching weather for city: {city}");

                var url = $"{BaseUrl}?q={city}&appid={ApiKey}&units=metric";
                var result = await _httpClient.GetFromJsonAsync<WeatherResponse>(url);

                if (result == null)
                {
                    _logger.LogWarning($"Weather data for '{city}' was null.");
                }
                else
                {
                    _logger.LogInformation($"Successfully retrieved weather for: {city}");
                }

                return result;
            }
            catch (HttpRequestException ex)
            {
                // ✅ Log HTTP errors
                _logger.LogError(ex, $"HTTP error occurred while fetching weather for '{city}'.");
                return null;
            }
            catch (Exception ex)
            {
                // ✅ Log unexpected exceptions
                _logger.LogError(ex, $"Unexpected error occurred while fetching weather for '{city}'.");
                return null;
            }
        }
    }
}
