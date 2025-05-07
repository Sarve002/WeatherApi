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

            return Ok(weather); // Now returns the WeatherDto
        }

    }
}
