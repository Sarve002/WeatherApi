using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using WeatherApi.Models;
using WeatherApi.Services;
using WeatherApi.DTOs;
using Xunit;

namespace WeatherApi.Tests.Services
{
    public class WeatherServiceTests
    {
        [Fact] // This attribute marks the method as a unit test
        public async Task GetWeatherAsync_ReturnsWeatherDto_WhenCityIsValid()
        {
            // Arrange: Define a mock JSON response string that mimics OpenWeatherMap's actual API response
            // The keys here ("main", "temp", "weather", "wind") must match what the API sends
            // These keys will get mapped to your WeatherResponse.cs structure
            var expectedJson = @"
             {
                  ""Main"": { ""temp"": 80, ""humidity"": 60 },
                  ""Weather"": [{ ""main"": ""Clear"", ""description"": ""clear sky"" }],
                  ""Wind"": { ""speed"": 5 },
                  ""Name"": ""London""
             }";

            // Create a mocked HttpMessageHandler to intercept HTTP requests
            // We use Moq to override the SendAsync method so it returns our custom response
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",                             // Name of method to mock
                    ItExpr.IsAny<HttpRequestMessage>(),      // Any HTTP request
                    ItExpr.IsAny<CancellationToken>()        // Any cancellation token
                )
                .ReturnsAsync(new HttpResponseMessage       // Mocked response
                {
                    StatusCode = HttpStatusCode.OK,         // Simulate 200 OK
                    Content = new StringContent(expectedJson) // Simulate response body
                });

            // Create HttpClient with the mocked handler
            var httpClient = new HttpClient(handlerMock.Object);

            // Create a mock logger (ILogger is required by WeatherService)
            var loggerMock = new Mock<ILogger<WeatherService>>();

            // Instantiate the WeatherService with the mocked dependencies
            var service = new WeatherService(httpClient, loggerMock.Object);

            // Act: Call the method under test
            WeatherDto? result = await service.GetWeatherAsync("London");

            // Assert: Verify the response matches what we expect based on our mock JSON
            Assert.NotNull(result);                       // Check that the service didn't return null
            Assert.Equal(80f, result.Temperature);        // Matches the mocked temperature value
            Assert.Equal(60, result.Humidity);            // Matches the mocked humidity value
            Assert.Equal(5, result.WindSpeed);            // Matches the mocked wind speed value
            Assert.Equal("clear sky", result.Description); // Matches the mocked weather description
        }
    }
}
