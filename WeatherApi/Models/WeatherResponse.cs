namespace WeatherApi.Services
{
    public class WeatherResponse
    {
        public WeatherMainInfo Main { get; set; }              // Temperature and Humidity info
        public List<WeatherDescription> Weather { get; set; }  // Weather conditions (description, etc.)
        public WindInfo Wind { get; set; }                     // Wind information
        public string Name { get; set; }                       // City name

        // You can also flatten the properties for easier access like in the WeatherDto mapping
        public double Temperature => Main?.Temp ?? 0;
        public int Humidity => (int)(Main?.Humidity ?? 0); // Cast to int as it should be a percentage
        public double WindSpeed => Wind?.Speed ?? 0;
        public string Description => Weather?.FirstOrDefault()?.Description ?? string.Empty;
    }

    public class WeatherMainInfo
    {
        public double Temp { get; set; }       // Temperature in Celsius (OpenWeatherMap provides this as double)
        public double Humidity { get; set; }   // Humidity percentage (provided as double)
    }

    public class WindInfo
    {
        public double Speed { get; set; }      // Wind speed in meters/second (as a double)
    }

    public class WeatherDescription
    {
        public string Main { get; set; }         // Main weather condition (e.g., "Clear", "Rain")
        public string Description { get; set; }  // Detailed description (e.g., "clear sky")
        public string Icon { get; set; }         // Icon code (e.g., "01d" for a clear day)
    }

}
