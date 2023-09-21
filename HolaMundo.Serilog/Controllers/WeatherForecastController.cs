using Microsoft.AspNetCore.Mvc;

namespace HolaMundo.Serilog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            try
            {
                _logger.LogInformation("Executing WeatherForecast Get method");
                throw new Exception("Sample user created exception.", new Exception("Sample inner exception"));
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
               .ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, String.Format("Error/exception in WeatherForecast Get method. Error {0}", ex.Message));
                throw;
            }
        }
    }
}