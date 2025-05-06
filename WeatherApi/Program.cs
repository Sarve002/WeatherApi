using WeatherApi.Services;

namespace WeatherApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ? Logging is already configured by default in ASP.NET Core
            // But you can explicitly set log level here:
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole(); // ? Ensure logging to console is enabled
            builder.Logging.SetMinimumLevel(LogLevel.Information); // ? Show Information and above
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient<WeatherService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
