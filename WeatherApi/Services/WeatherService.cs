using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DotNetEnv;
using WeatherApi.Models;
using Microsoft.Extensions.Logging; // Import ILogger
using WeatherApi.DTOs;  // Import DTO namespace
using WeatherApi.Services;  // Import the WeatherResponse model namespace


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
        public async Task<WeatherDto?> GetWeatherAsync(string city)
        {
            try
            {
                _logger.LogInformation($"Fetching weather for city: {city}");

                var url = $"{BaseUrl}?q={city}&appid={ApiKey}&units=metric";
                var response = await _httpClient.GetFromJsonAsync<WeatherResponse>(url); // Deserialize the response into WeatherResponse

                if (response == null)
                {
                    _logger.LogWarning($"Weather data for '{city}' was null.");
                    return null;
                }

                // Map to DTO
                var weatherDto = new WeatherDto
                {
                    City = city,
                    Temperature = response.Main.Temp,
                    Description = response.Weather[0].Description,
                    Humidity = response.Main.Humidity,
                    WindSpeed = response.Wind.Speed
                };

                _logger.LogInformation($"Successfully retrieved weather for: {city}");
                return weatherDto;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"HTTP error occurred while fetching weather for '{city}'.");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error occurred while fetching weather for '{city}'.");
                return null;
            }
        }
    }
}
