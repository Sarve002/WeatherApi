﻿namespace WeatherApi.Models
{
    public class WeatherResponse
    {
        public Main Main { get; set; }
        public List<Weather> Weather { get; set; }
        public string Name { get; set; }
    }

    public class Main
    {
        public float Temp { get; set; }
        public int Humidity { get; set; }
    }

    public class Weather
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }
}
