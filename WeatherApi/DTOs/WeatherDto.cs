namespace WeatherApi.DTOs
{
    // DTO to return only the relevant weather data to the client
    public class WeatherDto
    {
        public string City { get; set; }
        public string? Icon { get; set; }
        public double Temperature { get; set; }
        public string Description { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
    }
}
