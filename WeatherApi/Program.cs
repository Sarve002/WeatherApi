using WeatherApi.Services;

namespace WeatherApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000") // Allow React frontend
                              .AllowAnyHeader()                     // Allow any headers (e.g. Content-Type)
                              .AllowAnyMethod();                    // Allow GET, POST, etc.
                    });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
