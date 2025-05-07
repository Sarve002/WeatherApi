using Microsoft.AspNetCore.Mvc;
using WeatherApi.Services;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            var weather = await _weatherService.GetWeatherAsync(city);

            if (weather == null)
                return NotFound($"Weather data for '{city}' not found.");

            return Ok(weather);
        }

        // Endpoint for coordinates
        [HttpGet("coords")]
        public async Task<IActionResult> GetWeatherByCoords([FromQuery] double lat, [FromQuery] double lon)
        {
            var weather = await _weatherService.GetWeatherByCoordinatesAsync(lat, lon);

            if (weather == null)
                return NotFound($"Weather data for coordinates ({lat}, {lon}) not found.");

            return Ok(weather);
        }
    }
}
