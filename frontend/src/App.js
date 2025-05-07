import React, { useState } from 'react';
import WeatherForm from './WeatherForm';
import './App.css'; // For styling

function App() {
  const [weather, setWeather] = useState(null);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const [unit, setUnit] = useState('C');

  const fetchWeather = async (city) => {
    setLoading(true);
    try {
      const response = await fetch(`https://localhost:7091/api/weather/${city}`);
      if (!response.ok) {
        throw new Error('City not found');
      }
      const data = await response.json();
      setWeather(data);
      setError('');
    } catch (err) {
      setWeather(null);
      setError('Unable to fetch weather data. Please try again later.');
    } finally {
      setLoading(false);
    }
  };

  const getCurrentLocationWeather = () => {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(async (position) => {
        const { latitude, longitude } = position.coords;
        try {
          const response = await fetch(`https://localhost:7091/api/Weather/coords?lat=${latitude}&lon=${longitude}`);
          if (!response.ok) {
            throw new Error("Failed to fetch weather data from coordinates.");
          }
          const data = await response.json();
          setWeather(data);
          setError('');
        } catch (err) {
          console.error(err);
          setWeather(null);
          setError("Unable to fetch location weather. Please try again later.");
        }
      }, () => {
        setError("Unable to fetch location.");
      });
    } else {
      setError("Geolocation is not supported by this browser.");
    }
  };

  const toggleUnit = () => {
    setUnit(unit === 'C' ? 'F' : 'C');
  };

  const convertTemperature = (tempC) => {
    return unit === 'F' ? (tempC * 9/5) + 32 : tempC;
  };

  return (
    <div className="app-container">
      <h1>Weather App</h1>
      <WeatherForm onSearch={fetchWeather} />

      <div className="button-container">
        <button onClick={getCurrentLocationWeather}>Get Current Location Weather</button>
        <button onClick={toggleUnit}>
          Toggle Unit ({unit === 'C' ? 'Celsius' : 'Fahrenheit'})
        </button>
      </div>

      {loading && <p>Loading...</p>}
      {error && <p style={{ color: 'red' }}>{error}</p>}

      {weather && (
        <div className="weather-info">
          <h2>{weather.city}</h2>
          <p>{weather.description}</p>
          <p>Temp: {convertTemperature(weather.temperature).toFixed(1)}°{unit}</p>
          <p>Humidity: {weather.humidity}%</p>
          <p>Wind: {weather.windSpeed} m/s</p>
          <img
            src={`https://openweathermap.org/img/wn/${weather.icon}@4x.png`}
            alt="Weather Icon"
            className="weather-icon"
          />
        </div>
      )}
    </div>
  );
}

export default App;
