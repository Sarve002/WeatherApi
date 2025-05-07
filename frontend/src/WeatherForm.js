// WeatherForm.js
import React, { useState } from 'react';
import './styles/WeatherForm.css'; // Importing the CSS

function WeatherForm({ onSearch }) {
  const [city, setCity] = useState('');

  const handleSubmit = (event) => {
    event.preventDefault();
    if (city) {
      onSearch(city); // Passing city to fetch weather
    }
  };

  return (
    <div className="weather-form">
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          value={city}
          onChange={(e) => setCity(e.target.value)}
          placeholder="Enter city"
        />
        <button type="submit">Get Weather</button>
      </form>
    </div>
  );
}

export default WeatherForm;

