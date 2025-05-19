# 🌤 WeatherApi

**WeatherApi** is a full-stack weather forecast application built using **ASP.NET Core 8 (C#)** for the backend and **React** for the frontend. It consumes a third-party weather API and displays current and forecasted weather data in a modern UI.

### Screenshot

Here’s a screenshot of the weather app running:

![WeatherApp-Screenshot](https://github.com/user-attachments/assets/d1b4b7b7-bf62-4f25-9625-6f2cdceeb7ed)


## 🔧 Backend (.NET) Details

### Technologies Used
- ASP.NET Core 8
- C#
- RESTful API Architecture
- JSON-based configuration
- Dependency Injection
- DTO Pattern
- Weather API Integration

### Key Features
- Fetches weather data from an external API
- Clean architecture using services, DTOs, and models
- API endpoints via Controllers
- Configurable through `appsettings.json`
- Structured logging and environment setup

### Notable Files
- `Program.cs` - Main app entry
- `Controllers/WeatherController.cs` - Handles API routes
- `Services/WeatherService.cs` - Core logic for weather fetching
- `Models/` and `DTOs/` - Typed classes for clean data transfer
- `Properties/launchSettings.json` - Development configuration



## 🌐 Frontend (React) Details

### Technologies Used
- React
- TypeScript
- Tailwind CSS
- ESLint for linting and best practices
- Likely uses Axios or Fetch for API calls

### Key Features
- Displays weather information in a responsive layout
- Connects to the .NET backend to retrieve data
- Tailwind CSS used for modern styling
- Organized frontend architecture with reusable components



## 📂 Project Structure

```
WeatherApi/
├── WeatherApi/                # Backend (C# ASP.NET Core)
│   ├── Controllers/
│   ├── DTOs/
│   ├── Models/
│   ├── Services/
│   ├── Properties/
│   ├── appsettings.json
│   ├── WeatherApi.csproj
│   └── Program.cs
└── frontend/frontend-react/   # Frontend (React)
    ├── node_modules/
    ├── public/
    ├── src/
    └── package.json
```



## 🚀 Getting Started

### Backend Setup
```bash
cd WeatherApi/WeatherApi
dotnet restore
dotnet run
```

### Frontend Setup
```bash
cd frontend/frontend-react
npm install
npm start
```



## 📌 TODOs & Improvements

- Add error handling for failed API calls
- Unit tests for services and controllers
- Add environment-specific configurations for deployment
- Docker support for both frontend and backend



## 📃 License

This project is licensed under the MIT License.
